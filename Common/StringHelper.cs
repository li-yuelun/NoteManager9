using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class StringHelper
    {
        //将中文转换为英文缩写(例如：方加--fj)
        public static string ChineseToEnglish(string word)
        {
            long iCnChar; byte[] arrCN = System.Text.Encoding.Default.GetBytes(word);
            //将传进来的参数转换为byte格式的，如果是英文字母，它的长度应该为1 
            //如果是中文，那它的长度应该是2	
            //如果是字母，则直接返回 
            if (arrCN.Length == 1)
            {
                word = word.ToUpper();
            }
            else
            {
                //是中文的话，长度就为2，分别接收到它的两个字节码 
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                //【<<】是位运算符，向左移动8位 
                iCnChar = (area << 8) + pos;
                //声明字母字符串已经对应位置的区间数组 
                string letter = "ABCDEFGHJKLMNOPQRSTWXYZ";
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52980, 53689, 54481, 55290 };
                for (int i = 0; i < 23; i++)
                {
                    //通过循环对比得到区间的位置，然后提取出相应位置的字母 
                    if (areacode[i] <= iCnChar && iCnChar < areacode[i + 1])
                    {
                        word = letter.Substring(i, 1);
                        break;
                    }
                    else
                    {
                        //如果无法识别（不在区间内，比如说￥、%、&等特殊符号） 
                        //就将它统一归类为【#】
                        word = "#";
                    }
                }
            }
            return word;
        }
    }
}
