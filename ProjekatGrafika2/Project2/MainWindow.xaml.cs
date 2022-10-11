using Project2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;


namespace Project2
{
    public partial class MainWindow : Window
    {
        public struct Coordinate
        {
            public int x, z;
        }
        private readonly Int32Collection vs =
            new Int32Collection {
                0,4,1,0,2,4,7,2,6,
                7,4,2,5,4,7,5,1,4,
                3,7,6,3,5,7,0,6,2,
                0,3,6,3,0,5,5,0,1
            };
        public const double  minLat = 45.2325,
                             maxLat = 45.277031,
                             minLon = 19.793909,
                             maxLon = 19.894459;
        private int idA = 0, idB = 0;
        private Brush cA, cB;
        private bool rotation = false;
        private bool loaded = false;
        private bool ttipsOpen = false;
        private long toletipsID ;
        private GeometryModel3D hitgeo;
        private int rotationInd = 0;
        public double noviX, noviZ;
        public const int mapw = 235, mapl = 155;
        private Point start = new Point();
        private Point3D rotationAxis;
        int[,] positions = new int[mapw,mapl];
        List<Point3D> ringAroundMap = new List<Point3D>();
        Dictionary<long, NewEntity> entities = new Dictionary<long, NewEntity>();
        Dictionary<long, GeometryModel3D> squares = new Dictionary<long, GeometryModel3D>();
        Dictionary<long, GeometryModel3D> threeDModels = new Dictionary<long, GeometryModel3D>();
        Dictionary<long, SwitchEntity> switches = new Dictionary<long, SwitchEntity>();
        Dictionary<long, LineEntity> lines = new Dictionary<long, LineEntity>();

        public MainWindow()
        {
            
            for (int i = 0; i < mapw; i++)
            {
                for (int j = 0; j < mapl; j++)
                {
                    positions[i,j] = -1;
                }
            }
            InitializeComponent();
            CalculateRotationAxis();
            CalculateRing3DPoints();

        }
        private void CalculateRotationAxis()
        {
            Point3D o = kamera.Position;
            Vector3D d = kamera.LookDirection;
            rotationAxis = new Point3D
                (
                o.X + d.X * (-(o.Y / d.Y)),
                0,
                o.Z + d.Z * (-(o.Y / d.Y))
                );
        }
        private void CalculateRing3DPoints()
        {
            ringAroundMap.Clear();
            
            Point3D rotationA  = rotationAxis;
            Point3D camPosition = kamera.Position;

            rotationA.Y += camPosition.Y;
            var ringRadius = Math.Abs(
                Math.Sqrt(
                    Math.Pow(camPosition.X - rotationA.X, 2)
                    +
                    Math.Pow(camPosition.Z - rotationA.Z, 2)
                ));

            for (double i = 0; i <= 2 * Math.PI; i += 0.001)
            {
                ringAroundMap.Add(
                    new Point3D(
                        rotationA.X + ringRadius* Math.Cos(i),
                        camPosition.Y,
                        rotationA.Z + ringRadius* Math.Sin(i)
                        )
                );
            }
        }
        private double GetX(double x)
        {
            var position = (x - minLon) / (maxLon - minLon);
            return mapw * position;
        }

        private double GetZ(double z)
        {
            var position = (z - minLat) / (maxLat - minLat); ;
            return mapl - mapl* position;
        }

        private Coordinate FindPosition(double x, double y)
        {
            Coordinate coordinate = new Coordinate();
            int integerX = (Int32)x - 1;
            int integerZ = (Int32)y - 1;
            if (integerX < 0)
            {
                integerX = 0;
            }
            if (integerZ < 0)
            {
                integerZ = 0;
            }
            positions[integerX, integerZ]++;
            coordinate.z = integerZ;
            coordinate.x = integerX;
            return coordinate;

        }

        private void viewport1_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Point3D o = kamera.Position; 
            Vector3D d = kamera.LookDirection;
            Point3D tackaPreseka = new Point3D(o.X + d.X * (-(o.Y / d.Y)) , 0, kamera.Position.Z + d.Z * (-(o.Y / d.Y)));
            if (e.Delta > 0)
            {
                kamera.Position += 0.05 * (tackaPreseka - kamera.Position);
            }
            else if (e.Delta <= 0)
            {
                kamera.Position -= 0.05 * (tackaPreseka - kamera.Position);
            }
            CalculateRing3DPoints();
        }

        private void viewport1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            viewport1.CaptureMouse();
            start = e.GetPosition(this);
        }

        private void viewport1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            viewport1.ReleaseMouseCapture();

        } 
        private void viewport1_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            viewport1.ReleaseMouseCapture();

        }

        private void viewport1_MouseMove(object sender, MouseEventArgs e)
        {
            if (viewport1.IsMouseCaptured && rotation)
            {
                Vector difference = e.GetPosition(this) - start;

                rotationInd += (int)(difference.Length);
                rotationInd %= ringAroundMap.Count;

                kamera.Position = new Point3D(
                     ringAroundMap[rotationInd].X,
                     kamera.Position.Y,
                     ringAroundMap[rotationInd].Z
                );

                kamera.LookDirection = rotationAxis - kamera.Position;
                CalculateRing3DPoints();
            }else if (viewport1.IsMouseCaptured)
            {
                Point difference = new Point(e.GetPosition(this).X - start.X, e.GetPosition(this).Y - start.Y);
                kamera.Position += (kamera.UpDirection * difference.Y * .000005 +
                    Vector3D.CrossProduct(kamera.LookDirection, kamera.UpDirection) * difference.X * -.00000005) * kamera.Position.Z;
                CalculateRing3DPoints();
            }
        }

        private void rbrg_Checked(object sender, RoutedEventArgs e)
        {
            rbrg.IsChecked = true;
            rbst.IsChecked = false;
            foreach (var item in entities.Values)
            {
                if (item.Type == 1) {
                    int num = entities[item.Id].Num;
                    ModelVisual3D obj = viewport1.Children[num] as ModelVisual3D;
                    GeometryModel3D gm = obj.Content as GeometryModel3D;
                    DiffuseMaterial dp = gm.Material as DiffuseMaterial;
                    SwitchEntity item1 = switches[item.Id];
                    if (item1.Status == "Open")
                        dp.Brush = Brushes.LightGreen;
                    else if (item1.Status == "Closed")
                        dp.Brush = Brushes.Red;
                }
            }
        }

        private void rbst_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var item in entities.Values)
            {
                if (item.Type == 1)
                {
                    ModelVisual3D obj = viewport1.Children[item.Num] as ModelVisual3D;
                    GeometryModel3D gm = obj.Content as GeometryModel3D;
                    DiffuseMaterial dp = gm.Material as DiffuseMaterial;
                    dp.Brush = Brushes.Purple;
                }
            }
            rbrg.IsChecked = false;
            rbst.IsChecked = true;
        }

        private void linesRB_Checked(object sender, RoutedEventArgs e)
        {
            vodnormal.IsChecked = false;
            vodavoda.IsChecked = true;
            
            foreach (var item in entities.Values)
            {
                if (item.Type == 3 && linesshow.IsChecked == true)
                {
                    int num = entities[item.Id].Num;
                    ModelVisual3D obj = viewport1.Children[num] as ModelVisual3D;
                    GeometryModel3D gm = obj.Content as GeometryModel3D;
                    DiffuseMaterial dp = gm.Material as DiffuseMaterial;
                    LineEntity item1 = lines[item.Id];
                    if (item1.R < 1 )
                        dp.Brush = Brushes.Red;
                    else if (item1.R>=1 && item1.R <= 2)
                        dp.Brush = Brushes.Orange;
                    else
                        dp.Brush = Brushes.YellowGreen;
                }
            }
        }

        private void linesRBD_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var item in entities.Values)
            {
                if (item.Type == 3 && linesshow.IsChecked==true)
                {
                    ModelVisual3D obj = viewport1.Children[item.Num] as ModelVisual3D;
                    GeometryModel3D gm = obj.Content as GeometryModel3D;
                    DiffuseMaterial dp = gm.Material as DiffuseMaterial;
                    LineEntity l = lines[item.Id];
                    if (l.ConductorMaterial == "Copper")
                        dp.Brush = Brushes.OrangeRed;
                    else if (l.ConductorMaterial == "Steel")
                        dp.Brush = Brushes.LightBlue;
                    else
                        dp.Brush = Brushes.Yellow;
                }
            }
            vodnormal.IsChecked = true;
            vodavoda.IsChecked = false;
        }

        private void hidelines(object sender, RoutedEventArgs e)
        {
            foreach (var item in switches.Values)
            {
                if (item.Status == "Open")
                {
                    int num = entities[item.Id].Num;
                    foreach (var lineee in lines.Values)
                    {
                        if (lineee.FirstEnd == item.Id)
                        {
                            ModelVisual3D obj = viewport1.Children[entities[lineee.Id].Num] as ModelVisual3D;
                            GeometryModel3D gm = obj.Content as GeometryModel3D;
                            DiffuseMaterial dp = gm.Material as DiffuseMaterial;
                            if (inactiveLines.IsChecked == true)
                            {
                                dp.Brush = Brushes.Transparent;
                                obj = viewport1.Children[entities[lineee.SecondEnd].Num] as ModelVisual3D;
                                gm = obj.Content as GeometryModel3D;
                                dp = gm.Material as DiffuseMaterial;
                                dp.Brush = Brushes.Transparent;
                            }else
                            {
                                if (lineee.ConductorMaterial == "Copper")
                                    dp.Brush = Brushes.OrangeRed;
                                else if (lineee.ConductorMaterial == "Steel")
                                    dp.Brush = Brushes.LightBlue;
                                else
                                    dp.Brush = Brushes.Yellow;
                                obj = viewport1.Children[entities[lineee.SecondEnd].Num] as ModelVisual3D;
                                gm = obj.Content as GeometryModel3D;
                                dp = gm.Material as DiffuseMaterial;
                                dp.Brush = Brushes.Purple;
                            }
                        }
                    }

                }
            }
        }
        

        private void viewport1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                rotation = true;
                viewport1.CaptureMouse();
                start = e.GetPosition(this);
            }
        }

        private void viewport1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if(MouseButtonState.Released == e.MiddleButton)
            {
                if (ttipsOpen == true)
                {
                    if (entities[toletipsID].ToleTips.IsOpen == true)
                    {
                        ttipsOpen = false;
                        entities[toletipsID].ToleTips.IsOpen = false;
                        return;
                    }else if (idA != 0)
                    {
                        ModelVisual3D obj = viewport1.Children[idA] as ModelVisual3D;
                        GeometryModel3D gm = obj.Content as GeometryModel3D;
                        DiffuseMaterial dp = gm.Material as DiffuseMaterial;
                        dp.Brush = cA;
                        obj = viewport1.Children[idB] as ModelVisual3D;
                        gm = obj.Content as GeometryModel3D;
                        dp = gm.Material as DiffuseMaterial;
                        dp.Brush = cB;
                    }
                }
                rotation = false;
                viewport1.ReleaseMouseCapture();
            }
        }

        private void showlines(object sender, RoutedEventArgs e)
        {
            if (linesshow.IsChecked == true)
            {
                foreach (var item in entities.Values)
                {
                    if (item.Type == 3)
                    {
                        int num = entities[item.Id].Num;
                        ModelVisual3D obj = viewport1.Children[num] as ModelVisual3D;
                        GeometryModel3D gm = obj.Content as GeometryModel3D;
                        DiffuseMaterial dp = gm.Material as DiffuseMaterial;
                        LineEntity l = lines[item.Id];
                        dp.Brush = Brushes.Transparent;

                        if (l.ConductorMaterial == "Copper")
                            dp.Brush = Brushes.OrangeRed;
                        else if (l.ConductorMaterial == "Steel")
                            dp.Brush = Brushes.LightBlue;
                        else
                            dp.Brush = Brushes.Yellow;
                        
                    }
                }
                
            }
            else
            {
                foreach (var item in entities.Values)
                {
                    if (item.Type == 3)
                    {
                        int num = entities[item.Id].Num;
                        ModelVisual3D obj = viewport1.Children[num] as ModelVisual3D;
                        GeometryModel3D gm = obj.Content as GeometryModel3D;
                        DiffuseMaterial dp = gm.Material as DiffuseMaterial;
                        dp.Brush = Brushes.Transparent;
                    }
                }
            }
        }
        private void ToleTips_MouseRightDown(object sender, MouseButtonEventArgs e)
        {
            Point mouseposition = e.GetPosition(viewport1);
            Point3D testpoint3D = new Point3D(mouseposition.X, mouseposition.Y, 0);
            Vector3D testdirection = new Vector3D(mouseposition.X, mouseposition.Y,0.1);
            PointHitTestParameters pointparams =
                     new PointHitTestParameters(mouseposition);
            RayHitTestParameters rayparams =
                     new RayHitTestParameters(testpoint3D, testdirection);

            viewport1.CaptureMouse();
            hitgeo = null;

            VisualTreeHelper.HitTest(viewport1, null, HTResult, pointparams);
        }
        private HitTestResultBehavior HTResult(System.Windows.Media.HitTestResult rawresult)
        {
            RayHitTestResult rayResult = rawresult as RayHitTestResult;

            if (rayResult != null)
            {
                bool gasit = false;
                foreach (long id in threeDModels.Keys)
                {
                    GeometryModel3D model = threeDModels[id];
                    if (model == rayResult.ModelHit && entities[id].Type!=3)
                    {
                        ttipsOpen = true;
                        hitgeo = (GeometryModel3D)rayResult.ModelHit;
                        gasit = true; 
                        entities[id].ToleTips.IsOpen = true;
                        entities[id].ToleTips.Placement = PlacementMode.MousePoint;
                        toletipsID = id;
                    }
                    else if (model == rayResult.ModelHit && entities[id].Type == 3 && linesshow.IsChecked==true)
                    {
                        toletipsID = id;

                        idA = entities[lines[id].FirstEnd].Num;
                        idB = entities[lines[id].SecondEnd].Num;

                        ModelVisual3D obj = viewport1.Children[idA] as ModelVisual3D;
                        GeometryModel3D gm = obj.Content as GeometryModel3D;
                        DiffuseMaterial dp = gm.Material as DiffuseMaterial;
                        cA = dp.Brush;
                        dp.Brush = Brushes.Lime;

                        obj = viewport1.Children[idB] as ModelVisual3D;
                        gm = obj.Content as GeometryModel3D;
                        dp = gm.Material as DiffuseMaterial;
                        cB = dp.Brush;
                        dp.Brush = Brushes.Lime;

                        ttipsOpen = true;
                    }
                }
                if (!gasit)
                {
                    hitgeo = null;
                }
            }

            return HitTestResultBehavior.Stop;
        }

        private void Load_Data(object sender, RoutedEventArgs e)
        {
            if (!loaded)
            {
                loaded = true;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("Geographic.xml");

                XmlNodeList nodeList;

                nodeList = xmlDoc.DocumentElement.SelectNodes("/NetworkModel/Nodes/NodeEntity");

                DirectionalLight directionalLight = new DirectionalLight();
                directionalLight.Direction = new Vector3D(0, 0, 0);
                foreach (XmlNode node in nodeList)
                {
                    NodeEntity nodeobj = new NodeEntity();
                    nodeobj.Id = long.Parse(node.SelectSingleNode("Id").InnerText);
                    nodeobj.Name = node.SelectSingleNode("Name").InnerText;
                    nodeobj.X = double.Parse(node.SelectSingleNode("X").InnerText);
                    nodeobj.Y = double.Parse(node.SelectSingleNode("Y").InnerText);

                    ToLatLon(nodeobj.X, nodeobj.Y, 34, out noviZ, out noviX);
                    if (noviZ > minLat && noviZ < maxLat && noviX > minLon && noviX < maxLon)
                    {
                        double newX = GetX(noviX), newZ = GetZ(noviZ);
                        Coordinate coordinate = FindPosition(newX, newZ);
                        int x = coordinate.x, z = coordinate.z;
                        int y = positions[x, z];
                        MeshGeometry3D square = new MeshGeometry3D();
                        square.Positions.Add(new Point3D(x, 0.1, z));
                        square.Positions.Add(new Point3D(x + 1, 0.1, z));
                        square.Positions.Add(new Point3D(x, 1.1 + y, z));
                        square.Positions.Add(new Point3D(x, 0.1, z + 1));
                        square.Positions.Add(new Point3D(x + 1, 1.1 + y, z));
                        square.Positions.Add(new Point3D(x + 1, 0.1, z + 1));
                        square.Positions.Add(new Point3D(x, 1.1 + y, z + 1));
                        square.Positions.Add(new Point3D(x + 1, 1.1 + y, z + 1));

                        

                        square.TriangleIndices = vs;
                        DiffuseMaterial dp = new DiffuseMaterial();
                        dp.Brush = Brushes.LightSkyBlue;
                        GeometryModel3D geometryModel3D = new GeometryModel3D();
                        geometryModel3D.Geometry = square;
                        geometryModel3D.Material = dp;

                        squares.Add(viewport1.Children.Count, geometryModel3D);
                        viewport1.Children.Add(new ModelVisual3D() { Content = geometryModel3D });

                        ToolTip toletips2 = new ToolTip();

                        toletips2.Content = "Node \n ID: " + nodeobj.Id + "  Name: " + nodeobj.Name;
                        toletips2.Background = System.Windows.Media.Brushes.Black;
                        toletips2.Foreground = System.Windows.Media.Brushes.White;
                        toletips2.BorderBrush = System.Windows.Media.Brushes.Black;

                        entities.Add(nodeobj.Id, new NewEntity(nodeobj.Id, x, z, 0, viewport1.Children.Count - 1, toletips2));
                        threeDModels.Add(nodeobj.Id, geometryModel3D);
                    }
                }


                nodeList = xmlDoc.DocumentElement.SelectNodes("/NetworkModel/Switches/SwitchEntity");
                foreach (XmlNode node in nodeList)
                {
                    SwitchEntity switchobj = new SwitchEntity();
                    switchobj.Id = long.Parse(node.SelectSingleNode("Id").InnerText);
                    switchobj.Name = node.SelectSingleNode("Name").InnerText;
                    switchobj.X = double.Parse(node.SelectSingleNode("X").InnerText);
                    switchobj.Y = double.Parse(node.SelectSingleNode("Y").InnerText);
                    switchobj.Status = node.SelectSingleNode("Status").InnerText;

                    ToLatLon(switchobj.X, switchobj.Y, 34, out noviZ, out noviX);

                    if (noviZ > minLat && noviZ < maxLat && noviX > minLon && noviX < maxLon)
                    {
                        if (switchobj.Status == "Open")
                        {
                            switchobj.Status = "Open";
                            SwitchEntity switchEntity = new SwitchEntity();
                            switchEntity.Id = switchobj.Id;
                            switchEntity.Name = switchobj.Name;
                            switchEntity.X = switchobj.X;
                            switchEntity.Y = switchobj.Y;
                            switchEntity.Status = "Open";
                            switches.Add(switchEntity.Id, switchEntity);
                        }
                        else
                        {
                            switches.Add(switchobj.Id, switchobj);

                        }
                        double newX = GetX(noviX), newZ = GetZ(noviZ);
                        Coordinate coordinate = FindPosition(newX, newZ);
                        int x = coordinate.x, z = coordinate.z;
                        int y = positions[x, z];
                        MeshGeometry3D square = new MeshGeometry3D();

                        square.Positions.Add(new Point3D(x, 0.1, z));
                        square.Positions.Add(new Point3D(x + 1, 0.1, z));
                        square.Positions.Add(new Point3D(x, 1.1 + y, z));
                        square.Positions.Add(new Point3D(x, 0.1, z + 1));
                        square.Positions.Add(new Point3D(x + 1, 1.1 + y, z));
                        square.Positions.Add(new Point3D(x + 1, 0.1, z + 1));
                        square.Positions.Add(new Point3D(x, 1.1 + y, z + 1));
                        square.Positions.Add(new Point3D(x + 1, 1.1 + y, z + 1));

                        
                        square.TriangleIndices = vs;
                        DiffuseMaterial dp = new DiffuseMaterial();
                        dp.Brush = Brushes.Purple;
                        GeometryModel3D geometryModel3D = new GeometryModel3D();
                        geometryModel3D.Geometry = square;
                        geometryModel3D.Material = dp;

                        squares.Add(viewport1.Children.Count, geometryModel3D);
                        viewport1.Children.Add(new ModelVisual3D() { Content = geometryModel3D });

                        ToolTip toletips2 = new ToolTip();

                        toletips2.Content = "Switch \n ID: " + switchobj.Id + "  Name: " + switchobj.Name + " Status: " + switchobj.Status;
                        toletips2.Background = System.Windows.Media.Brushes.Black;
                        toletips2.Foreground = System.Windows.Media.Brushes.White;
                        toletips2.BorderBrush = System.Windows.Media.Brushes.Black;

                        entities.Add(switchobj.Id, new NewEntity(switchobj.Id, x, z, 1, viewport1.Children.Count - 1, toletips2));
                        threeDModels.Add(switchobj.Id, geometryModel3D);
                    }
                }

                nodeList = xmlDoc.DocumentElement.SelectNodes("/NetworkModel/Substations/SubstationEntity");

                foreach (XmlNode node in nodeList)
                {
                    SubstationEntity sub = new SubstationEntity();
                    sub.Id = long.Parse(node.SelectSingleNode("Id").InnerText);
                    sub.Name = node.SelectSingleNode("Name").InnerText;
                    sub.X = double.Parse(node.SelectSingleNode("X").InnerText);
                    sub.Y = double.Parse(node.SelectSingleNode("Y").InnerText);

                    ToLatLon(sub.X, sub.Y, 34, out noviZ, out noviX);

                    if (noviZ > minLat && noviZ < maxLat && noviX > minLon && noviX < maxLon)
                    {
                        double newX = GetX(noviX), newZ = GetZ(noviZ);
                        Coordinate coordinate = FindPosition(newX, newZ);
                        int x = coordinate.x, z = coordinate.z;
                        int y = positions[x, z];
                        MeshGeometry3D square = new MeshGeometry3D();

                        square.Positions.Add(new Point3D(x, 0.1, z));
                        square.Positions.Add(new Point3D(x + 1, 0.1, z));
                        square.Positions.Add(new Point3D(x, 1.1 + y, z));
                        square.Positions.Add(new Point3D(x, 0.1, z + 1));
                        square.Positions.Add(new Point3D(x + 1, 1.1 + y, z));
                        square.Positions.Add(new Point3D(x + 1, 0.1, z + 1));
                        square.Positions.Add(new Point3D(x, 1.1 + y, z + 1));
                        square.Positions.Add(new Point3D(x + 1, 1.1 + y, z + 1));

                        square.TriangleIndices = vs;
                        DiffuseMaterial dp = new DiffuseMaterial();
                        dp.Brush = Brushes.Yellow;
                        GeometryModel3D geometryModel3D = new GeometryModel3D();

                        geometryModel3D.Geometry = square;
                        geometryModel3D.Material = dp;

                        squares.Add(viewport1.Children.Count, geometryModel3D);
                        viewport1.Children.Add(new ModelVisual3D() { Content = geometryModel3D });

                        ToolTip toletips2 = new ToolTip();

                        toletips2.Content = "Substation\nID: " + sub.Id + "  Name: " + sub.Name;
                        toletips2.Background = System.Windows.Media.Brushes.Black;
                        toletips2.Foreground = System.Windows.Media.Brushes.White;
                        toletips2.BorderBrush = System.Windows.Media.Brushes.Black;

                        entities.Add(sub.Id, new NewEntity(sub.Id, x, z, 2, viewport1.Children.Count - 1, toletips2));

                        threeDModels.Add(sub.Id, geometryModel3D);
                    }

                }

                nodeList = xmlDoc.DocumentElement.SelectNodes("/NetworkModel/Lines/LineEntity");

                foreach (XmlNode node in nodeList)
                {

                    LineEntity l = new LineEntity();
                    l.Id = long.Parse(node.SelectSingleNode("Id").InnerText);
                    l.Name = node.SelectSingleNode("Name").InnerText;
                    if (node.SelectSingleNode("IsUnderground").InnerText.Equals("true"))
                    {
                        l.IsUnderground = true;
                    }
                    else
                    {
                        l.IsUnderground = false;
                    }
                    l.R = float.Parse(node.SelectSingleNode("R").InnerText);
                    l.ConductorMaterial = node.SelectSingleNode("ConductorMaterial").InnerText;
                    l.LineType = node.SelectSingleNode("LineType").InnerText;
                    l.ThermalConstantHeat = long.Parse(node.SelectSingleNode("ThermalConstantHeat").InnerText);
                    l.FirstEnd = long.Parse(node.SelectSingleNode("FirstEnd").InnerText);
                    l.SecondEnd = long.Parse(node.SelectSingleNode("SecondEnd").InnerText);

                    if (entities.ContainsKey(l.FirstEnd) && entities.ContainsKey(l.SecondEnd))
                    {
                        NewEntity startPoint = entities[l.FirstEnd];

                        int x = startPoint.X, z = startPoint.Z;
                        MeshGeometry3D square = new MeshGeometry3D();

                        square.Positions.Add(new Point3D(x + 0.3, 0.1, z));
                        square.Positions.Add(new Point3D(x + 0.3, 0.4, z));
                        square.Positions.Add(new Point3D(x + 0.6, 0.1, z));
                        square.Positions.Add(new Point3D(x + 0.6, 0.4, z));
                        int i = 1;
                        int j = 4;

                        foreach (XmlNode pointNode in node.ChildNodes[9].ChildNodes) // 9 posto je Vertices 9. node u jednom line objektu
                        {
                            Point p = new Point();
                            p.X = double.Parse(pointNode.SelectSingleNode("X").InnerText);
                            p.Y = double.Parse(pointNode.SelectSingleNode("Y").InnerText);


                            ToLatLon(p.X, p.Y, 34, out noviZ, out noviX);
                            if (noviZ > minLat && noviZ < maxLat && noviX > minLon && noviX < maxLon)
                            {
                                double newX = GetX(noviX), newZ = GetZ(noviZ);
                                Coordinate coordinate = FindPosition(newX, newZ);
                                p = new Point(coordinate.x, coordinate.z);

                                square.Positions.Add(new Point3D(p.X + 0.3, 0.1, p.Y));
                                square.Positions.Add(new Point3D(p.X + 0.3, 0.4, p.Y));
                                square.Positions.Add(new Point3D(p.X + 0.6, 0.1, p.Y));
                                square.Positions.Add(new Point3D(p.X + 0.6, 0.4, p.Y));

                                Int32Collection vsbs = new Int32Collection
                                {
                                    j*i,j*i+1,j*i-3,
                                    j*i,j*i-3,j*i-4,
                                    j*i-1,j*i-3,j*i+1,
                                    j*i-1,j*i+1,j*i+3,
                                    j*i-2,j*i-1,j*i+2,
                                    j*i+2,j*i-1,j*i+3,
                                    j*i-3,j*i+1,j*i-4,
                                    j*i-4,j*i+1,j*i,
                                    j*i+3,j*i+1,j*i-3,
                                    j*i+3,j*i-3,j*i-1,
                                    j*i+3,j*i-1,j*i-2,
                                    j*i+3,j*i-2,j*i+2
                                };
                                foreach (var item in vsbs)
                                {
                                    square.TriangleIndices.Add(item);
                                }
                                i++;
                            }

                        }
                        NewEntity endPoint = entities[l.SecondEnd];

                        int xx = endPoint.X, zz = endPoint.Z;

                        square.Positions.Add(new Point3D(xx + 0.3, 0.1, zz));
                        square.Positions.Add(new Point3D(xx + 0.3, 0.4, zz));
                        square.Positions.Add(new Point3D(xx + 0.6, 0.1, zz));
                        square.Positions.Add(new Point3D(xx + 0.6, 0.4, zz));

                        Int32Collection vs1 = new Int32Collection
                            {
                                j*i,j*i+1,j*i-3,
                                j*i,j*i-3,j*i-4,
                                j*i-1,j*i-3,j*i+1,
                                j*i-1,j*i+1,j*i+3,
                                j*i-2,j*i-1,j*i+2,
                                j*i+2,j*i-1,j*i+3,
                                j*i-3,j*i+1,j*i-4,
                                j*i-4,j*i+1,j*i,
                                j*i+3,j*i+1,j*i-3,
                                j*i+3,j*i-3,j*i-1,
                                j*i+3,j*i-1,j*i-2,
                                j*i+3,j*i-2,j*i+2
                            };
                        foreach (var item in vs1)
                        {
                            square.TriangleIndices.Add(item);
                        }

                        DiffuseMaterial dp = new DiffuseMaterial();
                        if (l.ConductorMaterial == "Copper")
                        {
                            dp.Brush = Brushes.OrangeRed;
                        }
                        else if (l.ConductorMaterial == "Steel")
                        {
                            dp.Brush = Brushes.LightBlue;
                        }
                        else { dp.Brush = Brushes.Yellow; }
                        GeometryModel3D geometryModel3D = new GeometryModel3D();

                        geometryModel3D.Geometry = square;
                        geometryModel3D.Material = dp;

                        lines.Add(l.Id, l);
                        entities.Add(l.Id, new NewEntity(l.Id, x, z, 3, viewport1.Children.Count - 1, new ToolTip()));

                        viewport1.Children.Add(new ModelVisual3D() { Content = geometryModel3D });
                        threeDModels.Add(l.Id, geometryModel3D);
                    }
                }
            }
        }
        //From UTM to Latitude and longitude in decimal
        public static void ToLatLon(double utmX, double utmY, int zoneUTM, out double latitude, out double longitude)
        {
            bool isNorthHemisphere = true;

            var diflat = -0.00066286966871111111111111111111111111;
            var diflon = -0.0003868060578;

            var zone = zoneUTM;
            var c_sa = 6378137.000000;
            var c_sb = 6356752.314245;
            var e2 = Math.Pow((Math.Pow(c_sa, 2) - Math.Pow(c_sb, 2)), 0.5) / c_sb;
            var e2cuadrada = Math.Pow(e2, 2);
            var c = Math.Pow(c_sa, 2) / c_sb;
            var x = utmX - 500000;
            var y = isNorthHemisphere ? utmY : utmY - 10000000;

            var s = ((zone * 6.0) - 183.0);
            var lat = y / (c_sa * 0.9996);
            var v = (c / Math.Pow(1 + (e2cuadrada * Math.Pow(Math.Cos(lat), 2)), 0.5)) * 0.9996;
            var a = x / v;
            var a1 = Math.Sin(2 * lat);
            var a2 = a1 * Math.Pow((Math.Cos(lat)), 2);
            var j2 = lat + (a1 / 2.0);
            var j4 = ((3 * j2) + a2) / 4.0;
            var j6 = ((5 * j4) + Math.Pow(a2 * (Math.Cos(lat)), 2)) / 3.0;
            var alfa = (3.0 / 4.0) * e2cuadrada;
            var beta = (5.0 / 3.0) * Math.Pow(alfa, 2);
            var gama = (35.0 / 27.0) * Math.Pow(alfa, 3);
            var bm = 0.9996 * c * (lat - alfa * j2 + beta * j4 - gama * j6);
            var b = (y - bm) / v;
            var epsi = ((e2cuadrada * Math.Pow(a, 2)) / 2.0) * Math.Pow((Math.Cos(lat)), 2);
            var eps = a * (1 - (epsi / 3.0));
            var nab = (b * (1 - epsi)) + lat;
            var senoheps = (Math.Exp(eps) - Math.Exp(-eps)) / 2.0;
            var delt = Math.Atan(senoheps / (Math.Cos(nab)));
            var tao = Math.Atan(Math.Cos(delt) * Math.Tan(nab));

            longitude = ((delt * (180.0 / Math.PI)) + s) + diflon;
            latitude = ((lat + (1 + e2cuadrada * Math.Pow(Math.Cos(lat), 2) - (3.0 / 2.0) * e2cuadrada * Math.Sin(lat) * Math.Cos(lat) * (tao - lat)) * (tao - lat)) * (180.0 / Math.PI)) + diflat;
        }
    }
}
