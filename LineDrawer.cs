using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Camera))]
public class LineDrawer : MonoBehaviour 
{
    public Color lineColor;
    public Material lineMaterial = null;

    private List<Line> _lines;

    /// <summary>
    /// Init the list and the material
    /// </summary>
    private void Start()
    {
        _lines = new List<Line>();

        if (lineMaterial == null)
        {
            lineMaterial = new Material("Shader \"Lines/Colored Blended\" {" +
                "SubShader { Pass { " +
                "    Blend SrcAlpha OneMinusSrcAlpha " +
                "    ZWrite On Cull Off Fog { Mode Off } " +
                "    BindChannels {" +
                "      Bind \"vertex\", vertex Bind \"color\", color }" +
                "} } }");
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
        }
    }

    /// <summary>
    /// Draw the stuff
    /// </summary>
    private void OnPostRender()
    {
        GL.PushMatrix();

        lineMaterial.SetPass(0);

        GL.LoadOrtho();

        GL.Color(lineColor);

        GL.Begin(GL.LINES);
        {
            foreach (Line line in _lines)
                line.Draw();
        }
        GL.End();

        GL.PopMatrix();
    }

    /// <summary>
    /// Helper class to draw a line
    /// </summary>
    protected struct Line
    {
        private Vector2 _from, _to;

        /// <summary>
        /// 2D constructor
        /// </summary>
        /// <param name="f">From position, viewport coord.</param>
        /// <param name="to">To position, viewport coord.</param>
        public Line(Vector2 from, Vector2 to)
        {
            _from = from;
            _to = to;
        }

        /// <summary>
        /// 3D Constructor
        /// </summary>
        /// <param name="cam">Reference the the camera that will draw those lines</param>
        /// <param name="from">From position, world coord.</param>
        /// <param name="to">To position, world coord.</param>
        public Line(Camera cam, Vector3 from, Vector3 to)
            : this(cam.WorldToViewportPoint(from), cam.WorldToViewportPoint(to))
        {
        }

        /// <summary>
        /// GL calls to draw the lines. Z = 0
        /// </summary>
        public void Draw()
        {
            GL.Vertex3(_from.x, _from.y, 0f);
            GL.Vertex3(_to.x, _to.y, 0f);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("({0}, {1}) => ({2}, {3})",
                _from.x.ToString("f2"),
                _from.y.ToString("f2"),
                _to.x.ToString("f2"),
                _to.y.ToString("f2"));
        }
    }
}

