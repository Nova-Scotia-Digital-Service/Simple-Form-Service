using System.Globalization;

namespace SimpleFormsService.Domain;

public static class SystemTime
{
    public static Func<DateTime> Now = SystemTimeNow;
    public static string NowString = SystemTimeNow().ToString(CultureInfo.CurrentCulture);

    private static DateTime SystemTimeNow()
    {
        var ticks = DateTime.Now.Ticks;
        ticks -= ticks % TimeSpan.TicksPerSecond;

        return new DateTime(ticks);
    }

    public static void Reset()
    {
        Now = SystemTimeNow;
    }

    public static void Substitute(DateTime when)
    {
        Now = () => when;
    }
}