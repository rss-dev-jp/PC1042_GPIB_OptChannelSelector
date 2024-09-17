using System.Collections.Generic;
using System.Windows;

namespace RssDev.Common.RenderUtility
{
    /// <summary>
    /// 文字セグメント
    /// </summary>
    public class StrSegment
    {
        private int[,] STR_0 = new int[,]
        {
            {0,1,1,1,0},
            {1,0,0,0,1},
            {1,0,0,0,1},
            {1,0,0,0,1},
            {0,1,1,1,0},        
        };

        private int[,] STR_1 = new int[,]
        {
            {0,0,0,0,1},
            {0,0,0,0,1},
            {0,0,0,0,1},
            {0,0,0,0,1},
            {0,0,0,0,1},        
        };

        private int[,] STR_2 = new int[,]
        {
            {0,1,1,1,0},
            {0,0,0,0,1},
            {0,0,1,1,0},
            {0,1,0,0,0},
            {1,1,1,1,1},        
        };

        private int[,] STR_3 = new int[,]
        {
            {1,1,1,1,1},
            {0,0,0,0,1},
            {1,1,1,1,1},
            {0,0,0,0,1},
            {1,1,1,1,1},
        };

        private int[,] STR_4 = new int[,]
        {
            {1,0,0,1,0},
            {1,0,0,1,0},
            {1,1,1,1,1},
            {0,0,0,1,0},
            {0,0,0,1,0},        
        };

        private int[,] STR_5 = new int[,]
        {
            {1,1,1,1,1},
            {1,0,0,0,0},
            {1,1,1,1,1},
            {0,0,0,0,1},
            {1,1,1,1,1},
        };

        private int[,] STR_6 = new int[,]
        {
            {1,1,1,1,1},
            {1,0,0,0,0},
            {1,1,1,1,1},
            {1,0,0,0,1},
            {1,1,1,1,1},
        };

        private int[,] STR_7 = new int[,]
        {
            {0,1,1,1,1},
            {0,0,0,0,1},
            {0,0,0,1,0},
            {0,0,1,0,0},
            {0,1,0,0,0},        
        };

        private int[,] STR_8 = new int[,]
        {
            {1,1,1,1,1},
            {1,0,0,0,1},
            {0,1,1,1,0},
            {1,0,0,0,1},
            {1,1,1,1,1},
        };

        private int[,] STR_9 = new int[,]
        {
            {1,1,1,1,1},
            {1,0,0,0,1},
            {1,1,1,1,1},
            {0,0,0,0,1},
            {0,0,0,0,1},
        };

        private int[,] STR_MINUS = new int[,]
        {
            {0,0,0,0,0},
            {0,0,0,0,0},
            {1,1,1,1,1},
            {0,0,0,0,0},
            {0,0,0,0,0},
        };

        private int[,] STR_DOT = new int[,]
        {
            {0,0,0,0,0},
            {0,0,0,0,0},
            {0,0,0,0,0},
            {0,0,0,1,1},
            {0,0,0,1,1},
        };

        private int[,] STR_NONE = new int[,]
        {
            {0,0,0,0,0},
            {0,0,0,0,0},
            {1,1,1,1,1},
            {0,0,0,0,0},
            {0,0,0,0,0},
        };

        private int[,] STR_LEFT = new int[,]
        {
            {0,0,0,0,1},
            {0,0,1,1,1},
            {0,1,1,1,1},
            {0,0,1,1,1},
            {0,0,0,0,1},
        };

        private int[,] STR_RIGHT = new int[,]
        {
            {1,0,0,0,0},
            {1,1,1,0,0},
            {1,1,1,1,0},
            {1,1,1,0,0},
            {1,0,0,0,0},
        };

        private int[,] STR_UP = new int[,]
        {
            {0,0,0,0,0},
            {0,0,1,0,0},
            {0,1,1,1,0},
            {0,1,1,1,0},
            {1,1,1,1,1},
        };

        private int[,] STR_DOWN = new int[,]
        {
            {1,1,1,1,1},
            {0,1,1,1,0},
            {0,1,1,1,0},
            {0,0,1,0,0},
            {0,0,0,0,0},
        };

        private int[,] STR_NEAR = new int[,]
        {
            {0,0,1,0,0},
            {0,1,0,1,0},
            {1,0,0,0,1},
            {0,1,0,1,0},
            {0,0,1,0,0},
        };

        private int[,] STR_FAR = new int[,]
        {
            {1,1,1,1,1},
            {1,0,0,0,1},
            {1,0,0,0,1},
            {1,0,0,0,1},
            {1,1,1,1,1},
        };

        private int[,] STR_NEAR2DOWN = new int[,]
        {
            {1,1,1,1,1},
            {0,1,1,1,0},
            {1,0,1,0,1},
            {0,1,1,1,0},
            {0,0,1,0,0},
        };

        private int[,] STR_A = new int[,]
        {
            {0,0,1,0,0},
            {0,1,0,1,0},
            {0,1,0,1,0},
            {1,1,1,1,1},
            {1,0,0,0,1},
        };
        private int[,] STR_B = new int[,]
        {
            {1,1,1,1,0},
            {1,0,0,0,1},
            {1,1,1,1,0},
            {1,0,0,0,1},
            {1,1,1,1,0},
        };
        private int[,] STR_C = new int[,]
        {
            {0,1,1,1,0},
            {1,0,0,0,1},
            {1,0,0,0,0},
            {1,0,0,0,1},
            {0,1,1,1,0},
        };
        private int[,] STR_D = new int[,]
        {
            {1,1,1,0,0},
            {1,0,0,1,0},
            {1,0,0,0,1},
            {1,0,0,1,0},
            {1,1,1,0,0},
        };

        private int[,] STR_E = new int[,]
        {
            {1,1,1,1,1},
            {1,0,0,0,0},
            {1,1,1,1,1},
            {1,0,0,0,0},
            {1,1,1,1,1},
        };

        private int[,] STR_F = new int[,]
        {
            {1,1,1,1,0},
            {1,0,0,0,0},
            {1,1,1,1,0},
            {1,0,0,0,0},
            {1,0,0,0,0},
        };

        private int[,] STR_G = new int[,]
        {
            {0,1,1,1,0},
            {1,0,0,0,0},
            {1,0,0,1,1},
            {1,0,0,0,1},
            {0,1,1,1,0},
        };

        private int[,] STR_H = new int[,]
        {
            {1,0,0,0,1},
            {1,0,0,0,1},
            {1,1,1,1,1},
            {1,0,0,0,1},
            {1,0,0,0,1},
        };

        private int[,] STR_I = new int[,]
        {
            {0,1,1,1,0},
            {0,0,1,0,0},
            {0,0,1,0,0},
            {0,0,1,0,0},
            {0,1,1,1,0},
        };

        private int[,] STR_J = new int[,]
        {
            {1,1,1,1,1},
            {0,0,1,0,0},
            {0,0,1,0,0},
            {1,0,1,0,0},
            {0,1,0,0,0},
        };

        private int[,] STR_K = new int[,]
        {
            {1,0,0,0,1},
            {1,0,0,1,0},
            {1,1,1,0,0},
            {1,0,0,1,0},
            {1,0,0,0,1},
        };

        private int[,] STR_L = new int[,]
        {
            {0,1,0,0,0},
            {0,1,0,0,0},
            {0,1,0,0,0},
            {0,1,0,0,0},
            {0,1,1,1,1},
        };

        private int[,] STR_M = new int[,]
        {
            {1,1,0,1,1},
            {1,1,1,1,1},
            {1,0,1,0,1},
            {1,0,0,0,1},
            {1,0,0,0,1},
        };

        private int[,] STR_N = new int[,]
        {
            {1,0,0,0,1},
            {1,1,0,0,1},
            {1,0,1,0,1},
            {1,0,0,1,1},
            {1,0,0,0,1},
        };

        private int[,] STR_O = new int[,]
        {
            {0,1,1,1,0},
            {1,0,0,0,1},
            {1,0,0,0,1},
            {1,0,0,0,1},
            {0,1,1,1,0},
        };

        private int[,] STR_P = new int[,]
        {
            {0,1,1,1,0},
            {0,1,0,0,1},
            {0,1,1,1,0},
            {0,1,0,0,0},
            {0,1,0,0,0},
        };

        private int[,] STR_Q = new int[,]
        {
            {0,1,1,1,0},
            {1,0,0,0,1},
            {1,0,1,0,1},
            {1,0,0,1,1},
            {0,1,1,1,1},
        };

        private int[,] STR_R = new int[,]
        {
            {0,1,1,1,0},
            {0,1,0,0,1},
            {0,1,1,1,0},
            {0,1,0,1,0},
            {0,1,0,0,1},
        };

        private int[,] STR_S = new int[,]
        {
            {0,0,1,1,0},
            {0,1,0,0,1},
            {0,0,1,0,0},
            {1,0,0,1,0},
            {0,1,1,1,0},
        };

        private int[,] STR_T = new int[,]
        {
            {1,1,1,1,1},
            {0,0,1,0,0},
            {0,0,1,0,0},
            {0,0,1,0,0},
            {0,0,1,0,0},
        };

        private int[,] STR_U = new int[,]
        {
            {1,0,0,0,1},
            {1,0,0,0,1},
            {1,0,0,0,1},
            {1,0,0,0,1},
            {0,1,1,1,0},
        };

        private int[,] STR_V = new int[,]
        {
            {1,0,0,0,1},
            {1,0,0,0,1},
            {0,1,0,1,0},
            {0,1,0,1,0},
            {0,0,1,0,0},
        };

        private int[,] STR_W = new int[,]
        {
            {1,0,1,0,1},
            {1,0,1,0,1},
            {0,1,0,1,0},
            {0,1,0,1,0},
            {0,1,0,1,0},
        };

        private int[,] STR_X = new int[,]
        {
            {1,0,0,0,1},
            {0,1,0,1,0},
            {0,0,1,0,0},
            {0,1,0,1,0},
            {1,0,0,0,1},
        };

        private int[,] STR_Y = new int[,]
        {
            {1,0,0,0,1},
            {0,1,0,1,0},
            {0,0,1,0,0},
            {0,0,1,0,0},
            {0,0,1,0,0},
        };

        private int[,] STR_Z = new int[,]
        {
            {1,1,1,1,1},
            {0,0,0,1,0},
            {0,0,1,0,0},
            {0,1,0,0,0},
            {1,1,1,1,1},
        };

        private int[,] STR_SPACE = new int[,]
        {
            {0,0,0,0,0},
            {0,0,0,0,0},
            {0,0,0,0,0},
            {0,0,0,0,0},
            {0,0,0,0,0},
        };

        private int[,] STR_EQUAL = new int[,]
        {
            {0,0,0,0,0},
            {0,1,1,1,0},
            {0,0,0,0,0},
            {0,1,1,1,0},
            {0,0,0,0,0},
        };

        private int[,] STR_COMMA = new int[,]
        {
            {0,0,0,0,0},
            {0,0,0,0,0},
            {0,0,0,0,0},
            {0,0,0,1,0},
            {0,0,1,0,0},
        };

        private List<List<Point>> strPointList;
        public int NumTextWidth { get; private set; }

        private List<List<Point>> minusPointList;
        private List<List<Point>> dotPointList;
        private List<List<Point>> spacePointList;
        private List<List<Point>> commaPointList;
        private List<List<Point>> equalPointList;

        private List<List<Point>> dirPointList;
        private List<List<Point>> alphabetPointList;

        /// <summary>
        /// 1文字あたりの縦横ドット数
        /// </summary>
        public int UnitSize { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="scale">拡大率 1=5dot</param>
        public StrSegment(int scale)
        {
            UnitSize = scale * STR_0.GetLength(0);

            List<int[,]> strList;
            strList = new List<int[,]>();
            strList.Add(STR_0);
            strList.Add(STR_1);
            strList.Add(STR_2);
            strList.Add(STR_3);
            strList.Add(STR_4);
            strList.Add(STR_5);
            strList.Add(STR_6);
            strList.Add(STR_7);
            strList.Add(STR_8);
            strList.Add(STR_9);

            CreateListPointList(scale, strList, out strPointList);
            NumTextWidth = STR_0.GetLength(1) * scale;

            strList = new List<int[,]>();
            strList.Add(STR_MINUS);
            CreateListPointList(scale, strList, out minusPointList);

            strList = new List<int[,]>();
            strList.Add(STR_DOT);
            CreateListPointList(scale, strList, out dotPointList);

            strList = new List<int[,]>();
            strList.Add(STR_NONE);
            strList.Add(STR_LEFT);
            strList.Add(STR_RIGHT);
            strList.Add(STR_UP);
            strList.Add(STR_DOWN);
            strList.Add(STR_NEAR);
            strList.Add(STR_FAR);
            strList.Add(STR_NEAR2DOWN);
            CreateListPointList(scale, strList, out dirPointList);

            strList = new List<int[,]>();
            strList.Add(STR_A);
            strList.Add(STR_B);
            strList.Add(STR_C);
            strList.Add(STR_D);
            strList.Add(STR_E);
            strList.Add(STR_F);
            strList.Add(STR_G);
            strList.Add(STR_H);
            strList.Add(STR_I);
            strList.Add(STR_J);
            strList.Add(STR_K);
            strList.Add(STR_L);
            strList.Add(STR_M);
            strList.Add(STR_N);
            strList.Add(STR_O);
            strList.Add(STR_P);
            strList.Add(STR_Q);
            strList.Add(STR_R);
            strList.Add(STR_S);
            strList.Add(STR_T);
            strList.Add(STR_U);
            strList.Add(STR_V);
            strList.Add(STR_W);
            strList.Add(STR_X);
            strList.Add(STR_Y);
            strList.Add(STR_Z);
            CreateListPointList(scale, strList, out alphabetPointList);

            strList = new List<int[,]>();
            strList.Add(STR_SPACE);
            CreateListPointList(scale, strList, out spacePointList);

            strList = new List<int[,]>();
            strList.Add(STR_EQUAL);
            CreateListPointList(scale, strList, out equalPointList);

            strList = new List<int[,]>();
            strList.Add(STR_COMMA);
            CreateListPointList(scale, strList, out commaPointList);
        }

        private void CreateListPointList(int scale, List<int[,]> strList, out List<List<Point>> strPointList)
        {
            strPointList = new List<List<Point>>();
            foreach (var str in strList)
            {
                var list = new List<Point>();
                for (int y = 0; y < str.GetLength(0); y++)
                {
                    for (int x = 0; x < str.GetLength(1); x++)
                    {
                        if (str[y, x] != 0)
                        {
                            for (int yy = 0; yy < scale; yy++)
                                for (int xx = 0; xx < scale; xx++)
                                    list.Add(new Point(x * scale + xx, y * scale + yy));
                        }
                    }
                }
                strPointList.Add(list);
            }
        }

        public List<Point> GetPointList(int num)
        {
            return strPointList[num];
        }

        public List<Point> GetAlphabetList(int num)
        {
            return alphabetPointList[num];
        }

        public List<Point> GetMinusList()
        {
            return minusPointList[0];
        }

        public List<Point> GetDotList()
        {
            return dotPointList[0];
        }

        public List<Point> GetEqualList()
        {
            return equalPointList[0];
        }

        public List<Point> GetCommaList()
        {
            return commaPointList[0];
        }

        public List<Point> GetSpaceList()
        {
            return spacePointList[0];
        }
/*
        public List<Point> GetDirList(MoveDirection moveDir)
        {
            return dirPointList[(int)moveDir];
        }
 */
    }
}
