using OpenTK;
using OpenTK.Graphics.OpenGL4;
using GLFW;
using GlmSharp;

using Shaders;
using Models;

namespace PMLabs
{
    //Implementacja interfejsu dostosowującego metodę biblioteki Glfw służącą do pozyskiwania adresów funkcji i procedur OpenGL do współpracy z OpenTK.
    public class BC: IBindingsContext
    {
        public IntPtr GetProcAddress(string procName)
        {
            return Glfw.GetProcAddress(procName);
        }
    }

    class Program
    {

		//static Torus torus = new Torus();
		//static Teapot teapot = new Teapot();
		//cw4,5
		static Sphere sphere = new Sphere(0.5f, 12, 12);
		static Sphere sphere2 = new Sphere(0.2f, 12, 12);
		static Sphere sphere3 = new Sphere(0.1f, 12, 12);

		static Sphere sphere4 = new Sphere(0.25f, 12, 12);
		static Sphere sphere5 = new Sphere(0.07f, 12, 12);
		//cw6
		/*
		static Torus torus = new Torus();
        static Torus torus2 = new Torus();*/



		public static void InitOpenGLProgram(Window window)
        {
            // Czyszczenie okna na kolor czarny
            GL.ClearColor(0, 0, 0, 1);

            // Ładowanie programów cieniujących
            DemoShaders.InitShaders("Shaders\\");
        }

        public static void DrawScene(Window window, float time)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit| ClearBufferMask.DepthBufferBit);

            //macierz widoku
            mat4 V = mat4.LookAt(
                new vec3(0.0f, 0.0f, -5.0f),
                new vec3(0.0f, 0.0f, 0.0f),
                new vec3(0.0f, 1.0f, 0.0f));
            mat4 P = mat4.Perspective(glm.Radians(50.0f), 1.0f, 1.0f, 50.0f);

			DemoShaders.spConstant.Use();
            GL.UniformMatrix4(DemoShaders.spConstant.U("P"), 1, false, P.Values1D);
            GL.UniformMatrix4(DemoShaders.spConstant.U("V"), 1, false, V.Values1D);



			//cw4,5
			mat4 M = mat4.Identity; //slonce
			//M *= mat4.Scale(new vec3(2.0f, 4.0f, 2.0f));
			//M *= mat4.Rotate(glm.Radians(60.0f * time), new vec3(0.0f, 1.0f, 0.0f));
			//M *= mat4.Rotate(glm.Radians(30.0f * time), new vec3(1.0f, 0.0f, 0.0f)); //300.0f zmienia szybkosc, im wyzsza wartosc tym szybciej sie obraca
			M *= mat4.Scale(new vec3(1.0f, 1.0f, 1.0f * glm.Sin(time)));
			GL.UniformMatrix4(DemoShaders.spConstant.U("M"), 1, false, M.Values1D);
			sphere.drawWire();

			mat4 M2 = M; //planeta
			M2 *= mat4.Rotate(glm.Radians(60.0f * time), new vec3(0.0f, 1.0f, 0.0f));
			M2 *= mat4.Translate(new vec3(1.5f, 0.0f, 0.0f));
			M2 *= mat4.Rotate(glm.Radians(60.0f * time), new vec3(0.0f, 1.0f, 0.0f));
			GL.UniformMatrix4(DemoShaders.spConstant.U("M"), 1, false, M2.Values1D);
			sphere2.drawWire();

			mat4 M3 = M2; //ksiezyc
			M3 *= mat4.Translate(new vec3(0.0f, 0.0f, 0.5f));
			M3 *= mat4.Rotate(glm.Radians(60.0f * time), new vec3(0.0f, 1.0f, 0.0f));
			GL.UniformMatrix4(DemoShaders.spConstant.U("M"), 1, false, M3.Values1D);
			sphere3.drawWire();

			mat4 M4 = M; //planeta2
			M4 *= mat4.Rotate(glm.Radians(60.0f * time), new vec3(0.0f, 0.0f, 1.0f));
			M4 *= mat4.Translate(new vec3(1.0f, 0.0f, 0.0f));
			M4 *= mat4.Rotate(glm.Radians(60.0f * time), new vec3(0.0f, 1.0f, 0.0f));
			GL.UniformMatrix4(DemoShaders.spConstant.U("M"), 1, false, M4.Values1D);
			sphere4.drawWire();

			mat4 M5 = M4; //ksiezyc2
			M5 *= mat4.Translate(new vec3(0.0f, 0.0f, 0.4f));
			M5 *= mat4.Rotate(glm.Radians(60.0f * time), new vec3(1.0f, 0.0f, 0.0f));
			GL.UniformMatrix4(DemoShaders.spConstant.U("M"), 1, false, M5.Values1D);
			sphere5.drawWire();


			//cw6
			/*mat4 M = mat4.Identity; 
            M *= mat4.Rotate(glm.Radians(60.0f*time), new vec3(0.0f, 0.0f, 1.0f));
            GL.UniformMatrix4(DemoShaders.spConstant.U("M"), 1, false, M.Values1D);
            torus.drawWire();

			mat4 M2 = mat4.Identity;
			M2 *= mat4.Translate(new vec3(-2.0f, 0.0f, 0.0f));
			M2 *= mat4.Rotate(glm.Radians(60.0f*time),new vec3(0.0f, 0.0f, -1.0f));
            //M2 *= mat4.Translate(new vec3(2.0f, 0.0f, 0.0f));
			GL.UniformMatrix4(DemoShaders.spConstant.U("M"), 1, false, M2.Values1D);
			torus2.drawWire();*/

			// TU RYSUJEMY
			//torus.drawWire(); //siatka
			//teapot.drawSolid(); //wypelnione
			//sphere.drawWire();
			//sphere2.drawWire();
			//sphere3.drawWire();

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
	DrawScene(window, (float)Glfw.Time);
	Glfw.PollEvents();
}


FreeOpenGLProgram(window);

Glfw.Terminate();


}


}
}