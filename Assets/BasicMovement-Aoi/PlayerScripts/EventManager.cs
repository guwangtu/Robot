﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    //C#单例
    static EventManager instance;
    public static EventManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EventManager();
            }
            return instance;
        }
    }
    EventManager() { }

    /*存储各种事件的字典  
     * 键为事件的标识符 比如 “添加分数”  “打开关卡门”
     * 值为一个委托 
     *    委托的返回值为任意类型，如果不需要返回值可以  return null
     *    委托的参数是  object[],  如果不需要参数可以填null  如果有1到多个参数，可以构建成object[] 
     *         比如：  new object[] {"空银子"，14，true};  使用时根据角标获取  objects[0]  objects[1]
     * 如果想节省object类型拆箱装箱的性能消耗，可以根据自己需求修改这个委托，我图方便就用object类型了        
     */
    public Dictionary<string, Func<object[], object>> events = new Dictionary<string, Func<object[], object>>();


    //注册事件的方法
    public void On(string sign, Func<object[], object> ac)
    {
        //判断是否已经存在这个标识符的事件   如果不存在就加入到字典里   
        if (events.ContainsKey(sign))
        {
            //委托的知识，如果不理解这步操作可以去复习一下
            events[sign] += ac;
        }
        else
        {
            events.Add(sign, ac);
        }
    }

    //执行事件的方法
    public object Event(string sign, object[] o)
    {
        if (events.ContainsKey(sign))
        {
            return events[sign](o);
        }
        else
        {   //算是安全校验， 个人写代码这方面积极性不是很足，不过最好还是尽量写一下
            Debug.Log("不存在标识为" + sign + "的事件，请先添加");
            return null;
        }
    }

}
