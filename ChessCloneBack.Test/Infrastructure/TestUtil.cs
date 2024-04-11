using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCloneBack.Test.Infrastructure
{
    public static class TestUtil
    {
        public static string FakePassword = "string";
        public static byte[] FakePasswordHash = [11, 254, 67, 123, 0, 176, 21, 231, 192, 23, 2, 167, 81, 71, 223, 17, 21, 79, 16, 97, 182, 234, 83, 58, 104, 99, 218, 116, 229, 191, 86, 98, 215, 67, 10, 147, 93, 29, 41, 92, 104, 84, 108, 255, 224, 148, 143, 157, 48, 254, 6, 103, 87, 166, 104, 104, 4, 58, 43, 155, 220, 198, 183, 84, 3, 209, 66, 79, 244, 6, 176, 5, 64, 234, 120, 192, 197, 11, 35, 105, 238, 54, 234, 86, 140, 161, 243, 82, 93, 5, 4, 249, 200, 162, 193, 131, 61, 33, 81, 174, 40, 254, 57, 117, 156, 43, 14, 82, 78, 213, 180, 188, 166, 214, 243, 130, 110, 97, 168, 158, 255, 21, 180, 97, 253, 197, 211, 22];

        public static string ExistingUserName = "Jill";
        public static string NewUserName = "Jack";
    }
}
