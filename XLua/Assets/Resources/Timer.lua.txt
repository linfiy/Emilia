local timer = CS.UniRx.Observable.Interval(CS.System.TimeSpan.FromSeconds(1));
num = 0;
function OnNext(a)
  num = num + 1
	print("定时器计数：" ..(num))
end
-- timer.Subscribe()
--(CS.UniRx.Observer.Create(typeof(CS.System.Int64))(OnNext))