using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPIExtension
{
    public static class TopographySurfaceExtension
    {
        public static List<XYZ> GetCornerPoints(this TopographySurface topo)
        {
            return topo.GetBoundaryPoints().ToList().SortCounterClockWise();
        }
    }
}
