using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using GameLoop;
using OpenTK;

namespace GameLoop
{
    sealed class Shader
    {
        private readonly int handle;

        public int Handle { get { return this.handle; } }

        public Shader(ShaderType type, string code)
        {
            // create shader object
            this.handle = GL.CreateShader(type);

            // set source and compile shader
            GL.ShaderSource(this.handle, code);
            GL.CompileShader(this.handle);
        }
    }

    sealed class ShaderProgram
    {
        private readonly int handle;

        public ShaderProgram(params Shader[] shaders)
        {
            // create program object
            this.handle = GL.CreateProgram();

            // assign all shaders
            foreach (var shader in shaders)
                GL.AttachShader(this.handle, shader.Handle);

            // link program (effectively compiles it)
            GL.LinkProgram(this.handle);

            // detach shaders
            foreach (var shader in shaders)
                GL.DetachShader(this.handle, shader.Handle);
        }

        public void Use()
        {
            // activate this program to be used
            GL.UseProgram(this.handle);
        }
        public int GetAttributeLocation(string name)
        {
            // get the location of a vertex attribute
            return GL.GetAttribLocation(this.handle, name);
        }

        public int GetUniformLocation(string name)
        {
            // get the location of a uniform variable
            return GL.GetUniformLocation(this.handle, name);
        }
    }

    sealed class Matrix4Uniform
    {
        private readonly string name;
        private Matrix4 matrix;

        public Matrix4 Matrix { get { return this.matrix; } set { this.matrix = value; } }

        public Matrix4Uniform(string name)
        {
            this.name = name;
        }

        public void Set(ShaderProgram program)
        {
            // get uniform location
            var i = program.GetUniformLocation(this.name);

            // set uniform value
            GL.UniformMatrix4(i, false, ref this.matrix);
        }
    }
}
