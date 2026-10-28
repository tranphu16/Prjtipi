using System;
using System.Collections.Generic;

    // Token: 0x020000E8 RID: 232
    public class XmapAlgorithm
    {
        // Token: 0x06000B77 RID: 2935 RVA: 0x000BB18C File Offset: 0x000B938C
        public static List<int> FindWay(int idMapStart, int idMapEnd)
        {
            List<int> wayPassedStart = XmapAlgorithm.GetWayPassedStart(idMapStart);
            return XmapAlgorithm.FindWay(idMapEnd, wayPassedStart);
        }

        // Token: 0x06000B78 RID: 2936 RVA: 0x000BB1B0 File Offset: 0x000B93B0
        private static List<int> FindWay(int idMapEnd, List<int> wayPassed)
        {
            int num = wayPassed[wayPassed.Count - 1];
            bool flag = num == idMapEnd;
            List<int> list;
            if (flag)
            {
                list = wayPassed;
            }
            else
            {
                bool flag2 = !XmapData.Instance().CanGetMapNexts(num);
                if (flag2)
                {
                    list = null;
                }
                else
                {
                    List<List<int>> list2 = new List<List<int>>();
                    List<MapNext> mapNexts = XmapData.Instance().GetMapNexts(num);
                    foreach (MapNext mapNext in mapNexts)
                    {
                        List<int> list3 = null;
                        bool flag3 = !wayPassed.Contains(mapNext.MapID);
                        if (flag3)
                        {
                            List<int> wayPassedNext = XmapAlgorithm.GetWayPassedNext(wayPassed, mapNext.MapID);
                            list3 = XmapAlgorithm.FindWay(idMapEnd, wayPassedNext);
                        }
                        bool flag4 = list3 != null;
                        if (flag4)
                        {
                            list2.Add(list3);
                        }
                    }
                    List<int> bestWay = XmapAlgorithm.GetBestWay(list2);
                    list = bestWay;
                }
            }
            return list;
        }

        // Token: 0x06000B79 RID: 2937 RVA: 0x000BB2A4 File Offset: 0x000B94A4
        private static List<int> GetBestWay(List<List<int>> ways)
        {
            bool flag = ways.Count == 0;
            List<int> list;
            if (flag)
            {
                list = null;
            }
            else
            {
                List<int> list2 = ways[0];
                for (int i = 1; i < ways.Count; i++)
                {
                    bool flag2 = XmapAlgorithm.IsWayBetter(ways[i], list2);
                    if (flag2)
                    {
                        list2 = ways[i];
                    }
                }
                list = list2;
            }
            return list;
        }

        // Token: 0x06000B7A RID: 2938 RVA: 0x000BB304 File Offset: 0x000B9504
        private static List<int> GetWayPassedStart(int idMapStart)
        {
            return new List<int> { idMapStart };
        }

        // Token: 0x06000B7B RID: 2939 RVA: 0x000BB328 File Offset: 0x000B9528
        private static List<int> GetWayPassedNext(List<int> wayPassed, int idMapNext)
        {
            return new List<int>(wayPassed) { idMapNext };
        }

        // Token: 0x06000B7C RID: 2940 RVA: 0x000BB34C File Offset: 0x000B954C
        private static bool IsWayBetter(List<int> way1, List<int> way2)
        {
            bool flag = XmapAlgorithm.IsBadWay(way1);
            bool flag2 = XmapAlgorithm.IsBadWay(way2);
            bool flag3 = flag && !flag2;
            bool flag4;
            if (flag3)
            {
                flag4 = false;
            }
            else
            {
                bool flag5 = !flag && flag2;
                if (flag5)
                {
                    flag4 = true;
                }
                else
                {
                    bool flag6 = way1.Count >= way2.Count;
                    flag4 = !flag6;
                }
            }
            return flag4;
        }

        // Token: 0x06000B7D RID: 2941 RVA: 0x000BB3AC File Offset: 0x000B95AC
        private static bool IsBadWay(List<int> way)
        {
            return XmapAlgorithm.IsWayGoFutureAndBack(way);
        }

        // Token: 0x06000B7E RID: 2942 RVA: 0x000BB3C4 File Offset: 0x000B95C4
        private static bool IsWayGoFutureAndBack(List<int> way)
        {
            List<int> list = new List<int> { 27, 28, 29 };
            for (int i = 1; i < way.Count - 1; i++)
            {
                bool flag = way[i] == 102 && way[i + 1] == 24 && list.Contains(way[i - 1]);
                if (flag)
                {
                    return true;
                }
            }
            return false;
        }
    }

