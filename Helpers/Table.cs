using OpenTK.Graphics.OpenGL4;
using System;

namespace Models
{
	public class Table
	{
		private int vertexBufferObject;
		private int vertexArrayObject;
		private int elementBufferObject;

		// Define the vertices, texture coordinates, normals, and indices for the table
		private float[] vertices = {
            // Positions          // Texture Coords
             0.5f,  0.0f,  0.5f,  1.0f, 1.0f, // Top Right
             0.5f,  0.0f, -0.5f,  1.0f, 0.0f, // Bottom Right
            -0.5f,  0.0f, -0.5f,  0.0f, 0.0f, // Bottom Left
            -0.5f,  0.0f,  0.5f,  0.0f, 1.0f  // Top Left 
        };

		private uint[] indices = {
			0, 1, 3, // First Triangle
            1, 2, 3  // Second Triangle
        };

		public Table()
		{
			// Generate and bind VAO
			vertexArrayObject = GL.GenVertexArray();
			GL.BindVertexArray(vertexArrayObject);

			// Generate, bind and fill VBO
			vertexBufferObject = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
			GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

			// Generate, bind and fill EBO
			elementBufferObject = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
			GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

			// Set vertex attribute pointers
			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
			GL.EnableVertexAttribArray(0);

			GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
			GL.EnableVertexAttribArray(1);

			// Unbind buffers
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
			GL.BindVertexArray(0);
		}

		public void Draw()
		{
			// Draw the table
			GL.BindVertexArray(vertexArrayObject);
			GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
			GL.BindVertexArray(0); // Unbind VAO
		}
	}
}
