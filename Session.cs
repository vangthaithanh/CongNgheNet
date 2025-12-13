using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnNet
{
    internal sealed class AuthUser
    {
        public int MaNV { get; set; }
        public string TaiKhoan { get; set; } = "";
        public string TenNV { get; set; } = "";
        public string Quyen { get; set; } = ""; // "Admin" hoặc "NhanVien"
    }

    internal static class Session
    {
        public static AuthUser CurrentUser { get; private set; }

        public static void SetUser(AuthUser user) => CurrentUser = user;

        public static void Clear() => CurrentUser = null;

        public static bool IsAdmin =>
            (CurrentUser?.Quyen ?? "").Trim().Equals("Admin", System.StringComparison.OrdinalIgnoreCase);
    }
}

