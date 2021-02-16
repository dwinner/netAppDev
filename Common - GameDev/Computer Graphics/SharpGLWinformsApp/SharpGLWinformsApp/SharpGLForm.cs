using System;
using System.Windows.Forms;
using SharpGL;

namespace SharpGLWinformsApplication1
{
   public partial class SharpGlForm : Form
   {
      /// <summary>
      ///    The current rotation.
      /// </summary>
      private float _rotation;

      /// <inheritdoc />
      public SharpGlForm()
      {
         InitializeComponent();
      }

      /// <summary>
      ///    Handles the OpenGLDraw event of the openGLControl control.
      /// </summary>
      /// <param name="sender">The source of the event.</param>
      /// <param name="e">The <see cref="RenderEventArgs" /> instance containing the event data.</param>
      private void OnDraw(object sender, RenderEventArgs e)
      {
         //  Get the OpenGL object.
         var gl = openGLControl.OpenGL;

         //  Clear the color and depth buffer.
         gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

         //  Load the identity matrix.
         gl.LoadIdentity();

         //  Rotate around the Y axis.
         gl.Rotate(_rotation, 0.0f, 1.0f, 0.0f);

         //  Draw a coloured pyramid.
         gl.Begin(OpenGL.GL_TRIANGLES);
         gl.Color(1.0f, 0.0f, 0.0f);
         gl.Vertex(0.0f, 1.0f, 0.0f);
         gl.Color(0.0f, 1.0f, 0.0f);
         gl.Vertex(-1.0f, -1.0f, 1.0f);
         gl.Color(0.0f, 0.0f, 1.0f);
         gl.Vertex(1.0f, -1.0f, 1.0f);
         gl.Color(1.0f, 0.0f, 0.0f);
         gl.Vertex(0.0f, 1.0f, 0.0f);
         gl.Color(0.0f, 0.0f, 1.0f);
         gl.Vertex(1.0f, -1.0f, 1.0f);
         gl.Color(0.0f, 1.0f, 0.0f);
         gl.Vertex(1.0f, -1.0f, -1.0f);
         gl.Color(1.0f, 0.0f, 0.0f);
         gl.Vertex(0.0f, 1.0f, 0.0f);
         gl.Color(0.0f, 1.0f, 0.0f);
         gl.Vertex(1.0f, -1.0f, -1.0f);
         gl.Color(0.0f, 0.0f, 1.0f);
         gl.Vertex(-1.0f, -1.0f, -1.0f);
         gl.Color(1.0f, 0.0f, 0.0f);
         gl.Vertex(0.0f, 1.0f, 0.0f);
         gl.Color(0.0f, 0.0f, 1.0f);
         gl.Vertex(-1.0f, -1.0f, -1.0f);
         gl.Color(0.0f, 1.0f, 0.0f);
         gl.Vertex(-1.0f, -1.0f, 1.0f);
         gl.End();

         #region How to create a shader source

         const string shaderSource = "some glsl source"; // NOTE: Here you can write your own glsl shader code
         var program = gl.CreateProgram();
         var shader = gl.CreateShader(OpenGL.GL_GEOMETRY_SHADER);
         gl.ShaderSource(shader, shaderSource);
         gl.CompileShader(shader);
         gl.AttachShader(program, shader);

         #endregion

         //  Nudge the rotation.
         _rotation += 3.0f;
      }

      /// <summary>
      ///    Handles the OpenGLInitialized event of the openGLControl control.
      /// </summary>
      /// <param name="sender">The source of the event.</param>
      /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
      private void OnInitialized(object sender, EventArgs e)
      {
         //  TODO: Initialise OpenGL here.

         //  Get the OpenGL object.
         var gl = openGLControl.OpenGL;

         //  Set the clear color.
         gl.ClearColor(0, 0, 0, 0);
      }

      /// <summary>
      ///    Handles the Resized event of the openGLControl control.
      /// </summary>
      /// <param name="sender">The source of the event.</param>
      /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
      private void openGLControl_Resized(object sender, EventArgs e)
      {
         //  TODO: Set the projection matrix here.

         //  Get the OpenGL object.
         var gl = openGLControl.OpenGL;

         //  Set the projection matrix.
         gl.MatrixMode(OpenGL.GL_PROJECTION);

         //  Load the identity.
         gl.LoadIdentity();

         //  Create a perspective transformation.
         gl.Perspective(60.0f, Width / (double) Height, 0.01, 100.0);

         //  Use the 'look at' helper function to position and aim the camera.
         gl.LookAt(-5, 5, -5, 0, 0, 0, 0, 1, 0);

         //  Set the modelview matrix.
         gl.MatrixMode(OpenGL.GL_MODELVIEW);
      }
   }
}