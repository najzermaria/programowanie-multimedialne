using OpenTK.Graphics.OpenGL4;
using GLFW;
using GlmSharp;
using Shaders;
using Models;
using OpenTK;

namespace PMLabs
{
    public class BC : IBindingsContext
    {
        public IntPtr GetProcAddress(string procName)
        {
            return Glfw.GetProcAddress(procName);
        }
    }

    class Program
    {

        static Cube cube1 = new Cube();
        static Cube cube2 = new Cube();
        static Cube cube3 = new Cube();
        static Cube cube4 = new Cube();
        static Cube cube5 = new Cube();
        static Teapot teapot = new Teapot();
        public static void InitOpenGLProgram(Window window)
        {
            // Czyszczenie okna na kolor czarny
            GL.ClearColor(0, 0, 0, 1);

            // Ładowanie programów cieniujących
            DemoShaders.InitShaders("Shaders\\");
        }

        public static void DrawScene(Window window)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            /*mat4 V = mat4.LookAt(
                new vec3(0.0f, 0.0f, -5.0f),  // Pozycja kamery (patrzy z przodu)
	            new vec3(0.0f, 1.0f, 0.0f),   // Nowy punkt docelowy (patrzy w prawo)
	            new vec3(0.0f, 0.0f, 1.0f));  // Wektor wskazujący w górę

			mat4 P = mat4.Perspective(glm.Radians(50.0f), 1.0f, 1.0f,50.0f);
*/
            /*mat4 P = mat4.Perspective(glm.Radians(50.0f), 1, 1, 50);
			mat4 V = mat4.LookAt(new vec3(0, 0, -8), new vec3(0, 0, 0), new vec3(0, 1f, 0)); //1-gdzie stoje, 2-na jaki pkt patrze, 3-ktora os to gora, jak jestem obrocona glowa
*/
            mat4 P = mat4.Perspective(glm.Radians(50.0f), -5, 1, 50); // Zachowaj to samo pole widzenia

            mat4 V = mat4.LookAt(
                new vec3(0, 0, -8),   // Pozycja kamery (patrzy na punkt (0, 0, 0))
                new vec3(0, 0, 0),   // Nowy punkt docelowy (patrzy wzdłuż osi X)
                new vec3(0, 1, 0));  // Wektor wskazujący w górę

            DemoShaders.spConstant.Use();
            GL.UniformMatrix4(DemoShaders.spConstant.U("P"), 1, false, P.Values1D);
            GL.UniformMatrix4(DemoShaders.spConstant.U("V"), 1, false, V.Values1D);

            /*
            mat4 M = mat4.Identity;
            GL.UniformMatrix4(DemoShaders.spConstant.U("M"), 1, false, M.Values1D);
            */
            //mat4 M = mat4.Identity;
            //mat4 M = mat4.Identity;

            //lewy dolna
            mat4 M = mat4.Translate(new vec3(-1.5f, -1.5f, 0.0f)) * mat4.RotateY(glm.Radians(45.0f)) * mat4.Scale(new vec3(0.2f, 0.2f, 1.0f));
            GL.UniformMatrix4(DemoShaders.spConstant.U("M"), 1, false, M.Values1D);
            cube1.drawWire();

            //  M2 *= mat4.Translate(new vec3(1.5f, 0.0f, 0.0f));

            //mat4 M = mat4.Translate(new vec3(2.0f, 0.0f, 0.0f)) * mat4.RotateY(glm.Radians(45.0f)) * mat4.Scale(new vec3(1.5f, 1.5f, 1.5f));

            // Sześcian 2 - prawa gorna dobrze
            M = mat4.Translate(new vec3(0.5f, 1.9f, 0.0f)) * mat4.RotateY(glm.Radians(45.0f)) * mat4.Scale(new vec3(0.2f, 0.2f, 1.0f));
            GL.UniformMatrix4(DemoShaders.spConstant.U("M"), 1, false, M.Values1D);
            cube2.drawWire();

            // Sześcian 3 - lewa gorna dobrze
            M = mat4.Translate(new vec3(-1.5f, 1.5f, 0.0f)) * mat4.RotateY(glm.Radians(45.0f)) * mat4.Scale(new vec3(0.2f, 0.2f, 1.0f));
            GL.UniformMatrix4(DemoShaders.spConstant.U("M"), 1, false, M.Values1D);
            cube3.drawWire();

            // Sześcian 4 - prawa dolna
            M = mat4.Translate(new vec3(0.5f, -2f, 0.0f)) * mat4.RotateY(glm.Radians(45.0f)) * mat4.Scale(new vec3(0.2f, 0.2f, 1.0f));
            GL.UniformMatrix4(DemoShaders.spConstant.U("M"), 1, false, M.Values1D);
            cube4.drawWire();

            // Sześcian 5 - blat
            M = mat4.Translate(new vec3(0.0f, 0.0f, 1.5f)) * mat4.RotateY(glm.Radians(45f)) * mat4.Scale(new vec3(2.0f, 2.0f, 0.1f));
            GL.UniformMatrix4(DemoShaders.spConstant.U("M"), 1, false, M.Values1D);
            cube5.drawWire();
            // TU RYSUJEMY

            teapot.drawSolid();

            Glfw.SwapBuffers(window);
        }

        public static void FreeOpenGLProgram(Window window)
        {
            // Możesz dodać odpowiednie czyszczenie zasobów tutaj, jeśli jest to konieczne
        }

        static void Main(string[] args)
        {
            Glfw.Init();

            Window window = Glfw.CreateWindow(500, 500, "Programowanie multimedialne", GLFW.Monitor.None, Window.None);

            Glfw.MakeContextCurrent(window);
            Glfw.SwapInterval(1);

            GL.LoadBindings(new BC()); //Pozyskaj adresy implementacji poszczególnych procedur OpenGL

            InitOpenGLProgram(window);

            Glfw.Time = 0;

            while (!Glfw.WindowShouldClose(window))
            {
                DrawScene(window);
                Glfw.PollEvents();
            }


            FreeOpenGLProgram(window);

            Glfw.Terminate();
        }


    }
}
