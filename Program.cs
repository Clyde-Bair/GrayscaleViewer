namespace GrayscaleViewer {
    internal class Program {
        static void Main(string[] args) {
            Console.CursorVisible = false;
            int ix = 0;
            int iy = 0;
            int b = 0;
            while (true) {
                /*
                string[] image = GetImage($"C:\\Users\\MCA-20\\Desktop\\Files\\Anim\\bmp{b}.bmp");
                Screen screen = new Screen();
                screen.Generate();
                for (int y = 0; y < image.Length; y++) {
                    for (int x = 0; x < image[y].Length; x++) {
                        screen.Draw(image[y][x], x + ix, y + iy);
                    }
                }
                screen.Refresh();
                */
                string[] image = GetImage($"C:\\Users\\MCA-20\\Desktop\\Files\\sa1.bmp");
                Screen screen = new Screen();
                screen.Generate();
                for (int y = 0; y < image.Length; y++) {
                    for (int x = 0; x < image[y].Length; x++) {
                        screen.Draw(image[y][x], x + ix, y + iy);
                    }
                }
                screen.Refresh();
                if (Console.KeyAvailable) {
                    var cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.W) {
                    iy++;
                }
                if (cki.Key == ConsoleKey.S) {
                    iy--;
                }
                if (cki.Key == ConsoleKey.A) {
                    ix++;
                }
                if (cki.Key == ConsoleKey.D) {
                    ix--;
                }
                }
                /*
                b++;
                if (b >= 23) {
                    b = 0;
                }
                Thread.Sleep(50);
                */
            }
            /*
            for (int i = 0; i < image.Length; i++) {
                Console.WriteLine(image[i]);
            }
            */
        }
        static string[] GetImage(string path) {
            /* ░▒▓█*/ /*51,102,153,204,255*/
            int width = 0;
            int height = 0;
            int color = 0;
            FileStream image = new FileStream($"{path}", FileMode.Open);
            for (int i = 0; i < 4; i++) {
                image.Position = 18 + i;
                width += image.ReadByte();
            }
            for (int i = 0; i < 4; i++) {
                image.Position = 22 + i;
                height += image.ReadByte();
            }
            image.Position = 54;
            string[] bmp = new string[height];
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    color = 0;
                    for (int c = 0; c < 3; c++) {
                        color += image.ReadByte();
                    }
                    color /= 3;
                    if (color > 204) {
                        bmp[y] += "█";
                    } else if (color > 153) {
                        bmp[y] += "▓";
                    } else if (color > 102) {
                        bmp[y] += "▒";
                    } else if (color > 51) {
                        bmp[y] += "░";
                    } else {
                        bmp[y] += " ";
                    }
                }
            }
            string[] render = new string[height];
            for (int b = 0; b < bmp.Length; b++) {
                int a = -b + bmp.Length - 1;
                render[b] = bmp[a];
            }
            image.Close();
            return render;
        }
    }
    public class Screen {
        public byte width = 0;
        public byte height = 0;
        public string[] screen = null!;
        public void Generate() {
            width = (byte)Console.WindowWidth;
            height = (byte)Console.WindowHeight;
            bool OddW = false; bool OddH = false;
            if (width % 2 == 0) { width--; OddW = true; }
            height -= 2; if (height % 2 == 0) { height--; OddH = true; }
            screen = new string[height];
            for (byte y = 0; y < height; y++) {
                for (byte x = 0; x < width; x++) {
                    screen[y] += " ";
                }
            }
            if (OddW) { width++; width /= 2; width--; } else { width /= 2; }
            if (OddH) { height++; height /= 2; height--; } else { height /= 2; }
        }
        public void Refresh() {
            string display = "";
            for (byte i = 0; i < screen.Count(); i++) {
                display += screen[i] + "\n";
            }
            Console.SetCursorPosition(0, 1);
            Console.Write(display);
        }
        public void Draw(char symb, int x, int y) {
            if (x >= screen[0].Length || x < 0) {
            } else if (y > screen.Length - 1 || y < 0) {
            } else {
                string line = "";
                for (int i = 0; i < screen[y].Length; i++) {
                    if (i == x) {
                        line += symb;
                    } else {
                        line += screen[y][i];
                    }
                }
                screen[y] = line;
            }
        }
    }
}
