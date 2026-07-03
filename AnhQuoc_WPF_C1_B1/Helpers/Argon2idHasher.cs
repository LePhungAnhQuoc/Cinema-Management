using System;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace AnhQuoc_WPF_C1_B1.Helpers
{
    public class Argon2idHasher
    {
        // Cấu hình tham số (Có thể điều chỉnh tùy thuộc vào cấu hình Server của bạn)
        private const int DegreeOfParallelism = 2;  // Sử dụng 2 luồng CPU
        private const int MemorySize = 65536;       // Sử dụng 64 MB RAM
        private const int Iterations = 4;           // Lặp 4 vòng
        private const int SaltSize = 16;            // Độ dài Salt (16 bytes)
        private const int HashSize = 32;            // Độ dài kết quả Hash (32 bytes)

        // 1. Hàm BĂM MẬT KHẨU (Dùng khi ĐĂNG KÝ)
        public static string HashPassword(string password)
        {
            // Tạo Salt ngẫu nhiên bằng bộ sinh số ngẫu nhiên an toàn mã hóa
            // 1. Khởi tạo mảng byte với kích thước SaltSize trước
            byte[] salt = new byte[SaltSize];

            // 2. Điền các giá trị ngẫu nhiên an toàn vào mảng vừa tạo
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            using (var argon2 = new Argon2id(passwordBytes))
            {
                argon2.Salt = salt;
                argon2.DegreeOfParallelism = DegreeOfParallelism;
                argon2.MemorySize = MemorySize;
                argon2.Iterations = Iterations;

                byte[] hash = argon2.GetBytes(HashSize);

                // Gộp Salt và Hash lại thành 1 chuỗi duy nhất để dễ lưu vào 1 cột trong CSDL
                // Định dạng: $argon2id$v=19$m=64384,t=4,p=2$[Chuỗi Salt Base64]$[Chuỗi Hash Base64]
                string saltBase64 = Convert.ToBase64String(salt);
                string hashBase64 = Convert.ToBase64String(hash);

                return $"$argon2id$m={MemorySize},t={Iterations},p={DegreeOfParallelism}${saltBase64}${hashBase64}";
            }
        }

        // 2. Hàm KIỂM TRA MẬT KHẨU (Dùng khi ĐĂNG NHẬP)
        public static bool VerifyPassword(string enteredPassword, string storedHashString)
        {
            try
            {
                // Tách chuỗi đã lưu trong CSDL để lấy các tham số, Salt và Hash cũ
                var parts = storedHashString.Split('$');
                if (parts.Length < 5 || parts[1] != "argon2id") return false;

                // Đọc cấu hình từ chuỗi (Hoặc dùng luôn cấu hình cứng ở trên nếu đồng nhất)
                var configParts = parts[2].Split(',');
                int memory = int.Parse(configParts[0].Split('=')[1]);
                int iterations = int.Parse(configParts[1].Split('=')[1]);
                int parallelism = int.Parse(configParts[2].Split('=')[1]);

                byte[] salt = Convert.FromBase64String(parts[3]);
                byte[] expectedHash = Convert.FromBase64String(parts[4]);
                byte[] enteredPasswordBytes = Encoding.UTF8.GetBytes(enteredPassword);

                // Tiến hành băm lại mật khẩu người dùng vừa nhập với Salt cũ
                using (var argon2 = new Argon2id(enteredPasswordBytes))
                {
                    argon2.Salt = salt;
                    argon2.DegreeOfParallelism = parallelism;
                    argon2.MemorySize = memory;
                    argon2.Iterations = iterations;

                    byte[] newHash = argon2.GetBytes(HashSize);

                    // So sánh an toàn (ngăn chặn Timing Attack)
                    return FixedTimeEqualsAlternative(newHash, expectedHash);
                }
            }
            catch
            {
                return false;
            }
        }
        private static bool FixedTimeEqualsAlternative(byte[] left, byte[] right)
        {
            if (left.Length != right.Length) return false;

            int accum = 0;
            for (int i = 0; i < left.Length; i++)
            {
                accum |= left[i] ^ right[i];
            }
            return accum == 0;
        }
    }
}