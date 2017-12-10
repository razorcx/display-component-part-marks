using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;

namespace DisplayComponentPartNumbers
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				var selection = new Picker().PickObjects(Picker.PickObjectsEnum.PICK_N_OBJECTS).ToList();
				if (selection == null) return;

				var connections = selection.OfType<Connection>().ToList();

				connections.ForEach(c =>
				{
					var drawer = new GraphicsDrawer();
					var color = new Color(1,1,1);

					//get parts from the connection
					var parts = c.GetChildren().ToList().OfType<Part>().ToList();

					parts.ForEach(p =>
					{
						var point = p.GetCenterLine(false)[0] as Point;
						var partMark = p.GetPartMark();
						drawer.DrawText(point, partMark, color);
					});
				});
			}
			catch (Exception ex)
			{
				
			}
		}
	}

	public static class TeklaModelExtensions
	{
		public static List<ModelObject> ToList(this ModelObjectEnumerator enumerator)
		{
			var modelObjects = new List<ModelObject>();
			while (enumerator.MoveNext())
			{
				var modelObject = enumerator.Current;
				modelObjects.Add(modelObject);
			}
			return modelObjects;
		}
	}
}
