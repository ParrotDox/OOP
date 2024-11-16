using PointLibrary;
UI_Point<double> UI = new UI_Point<double>();
UI.Execute();

//Найти самую удаленную от центра точку
//Создается массив для проверки
PointArray<double> pArr = new PointArray<double>(4, true);
pArr.Print();
//Создается точка с координатами центра
Point<double> center = new Point<double>(0, 0);
center.Print();
//С помощью итерационного цикла находится точка, находящаяся дальше всего от центра
double result = 0;
Point<double> farthestPoint = new Point<double>();
for(int p = 0; p < pArr.length; ++p) 
{
    double distanceBetween = PointStatic<double>.FindDistance(pArr[p], center);
    pArr[p].Print();
    Console.WriteLine($"Distance from center is {distanceBetween}");
    if ( distanceBetween > result) 
    {
        result = distanceBetween;
        farthestPoint = pArr[p];
    }
}
Console.WriteLine("Farthest point from center:");
farthestPoint.Print();
Console.WriteLine($"Result distance: {result}");