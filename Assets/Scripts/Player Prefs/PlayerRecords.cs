public class PlayerRecords
{
    //Level stats
    static ulong enemiesDefeated;
    static ulong pointsScored;
    static ulong shotsFired;
    static ulong wavesCleared;

    //Misc. stats
    static ulong timePlayedInSeconds;

    public class Clock
    {
        public uint years;
        public uint months;
        public uint weeks;
        public uint days;
        public uint hours;
        public ulong minutes;
        //iSeconds can be reused to get precise segments of time
        public ulong independentSeconds;
        public ulong seconds;

        public Clock(ulong seconds)
        {
            independentSeconds = seconds;
            //Divide it out from the top
            ulong totalTime = seconds;
            //Integers are truncated, therefore, the amount left over will be able to be subtracted easily
            years = (uint) (totalTime / (60 * 60 * 24 * 365));
            totalTime -= years * 60 * 60 * 24 * 365;
            //Given months are roughly 30 days
            months = (uint)(totalTime / (60 * 60 * 24 * 30));
            totalTime -= months * 60 * 60 * 24 * 30;
            days = (uint)(totalTime / (60 * 60 * 24));

        }

        public ulong GetHours()
        {
            return independentSeconds / (60 * 60);
        }
    }
}
