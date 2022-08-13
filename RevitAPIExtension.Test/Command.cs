using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;

namespace RevitAPIExtension.Test
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var p1 = new XYZ(1, -1, 0);
            var p2 = new XYZ(1, 1, 0);
            var p3 = new XYZ(-1, -1, 0);
            var p4 = new XYZ(-1, 1, 0);

            var p5 = new XYZ(5, 0, 0);
            var p6 = new XYZ(5, -1, 0);
            var p7 = new XYZ(-5, -1, 0);
            var p8 = new XYZ(-5, 1, 0);

            var pts = new List<XYZ>()
            {
                p1,p2,p3,p4,p5,p6,p7,p8
            };

            var orderedPts = pts.SortCounterClockWise();
            var uiDoc = commandData.Application.ActiveUIDocument;
            var doc = uiDoc.Document;

            var topo = doc.GetElement(uiDoc.Selection.PickObject(ObjectType.Element)) as TopographySurface;
            var lines = topo.GetCornerPoints().AsLines();
            var view = uiDoc.ActiveView;

            using (var trans = new Transaction(doc,"Hamada"))
            {
                trans.Start();
                lines.ForEach(line => doc.Create.NewDetailCurve(view, line));
                trans.Commit();
            }
           

            return Result.Succeeded;
        }
    }
}
