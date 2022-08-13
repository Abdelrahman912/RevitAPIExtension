using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPIExtension
{
    public static class XYZExtension
    {

        public static List<Line> AsLines(this List<XYZ> orderedPts)
        {
            var n = orderedPts.Count;
            var lines = orderedPts.Select((pt, i) =>
             {
                 var nextIndex = (i + 1) % n;
                 var line = Line.CreateBound(pt, orderedPts[nextIndex]);
                 return line;
             }).ToList();
            return lines;
        }

        public static XYZ AsNewWithZ(this XYZ point, double z) =>
            new XYZ(point.X, point.Y, z);

        public static List<XYZ> SortCounterClockWise(this List<XYZ> points)
        {
            if (points.Count < 3)
                return new List<XYZ>();

            //Get Center Point.
            var ptsOrderedByX = points.OrderBy(p => p.X);
            var minX = ptsOrderedByX.First().X;
            var maxX = ptsOrderedByX.Last().X;
            var cx = (minX + maxX) / 2;
            var ptsOrderedByY = points.OrderBy(p => p.Y);
            var minY = ptsOrderedByY.First().Y;
            var maxY = ptsOrderedByY.Last().Y;
            var cy = (minY + maxY) / 2;
            var center = new XYZ(cx, cy, 0);


            //Order by vector angle.
            Func<XYZ, double> orderFunc = pt =>
            {
                var vec = (pt.AsNewWithZ(0) - center).Normalize();
                var angle = Math.Atan2(vec.Y, vec.X);
                return angle;
            };

            var orderedPoints = points.OrderBy(orderFunc).ToList();
            return orderedPoints;
        }
    }
}
