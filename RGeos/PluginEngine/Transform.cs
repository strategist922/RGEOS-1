﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using RGeos.Geometry;
using RGeos.Core.PluginEngine;

namespace RGeos.PluginEngine
{
    public class Transform : IDisplayTransformation
    {
        public static float m_screenResolution = 96;
        public static PointF ToScreen(RPoint pt, UcMapControl mapCtrl)
        {
            PointF transformedPoint = new PointF((float)pt.X, (float)pt.Y);
            transformedPoint.Y = mapCtrl.ScreenHeight() - transformedPoint.Y;//将Unit坐标系转换为屏幕坐标系，Y轴反向，此时Y坐标为屏幕坐标系坐标
            transformedPoint.Y *= m_screenResolution * mapCtrl.Zoom;//相对于屏幕原点放大
            transformedPoint.X *= m_screenResolution * mapCtrl.Zoom;

            transformedPoint.X += mapCtrl.m_panOffset.X + mapCtrl.m_dragOffset.X;
            transformedPoint.Y += mapCtrl.m_panOffset.Y + mapCtrl.m_dragOffset.Y;
            return transformedPoint;
        }
        public static RPoint ToUnit(PointF screenpoint, UcMapControl mapCtrl)
        {
            float panoffsetX = mapCtrl.m_panOffset.X + mapCtrl.m_dragOffset.X;
            float panoffsetY = mapCtrl.m_panOffset.Y + mapCtrl.m_dragOffset.Y;
            float xpos = (screenpoint.X - panoffsetX) / (m_screenResolution * mapCtrl.Zoom);
            float ypos = mapCtrl.ScreenHeight() - ((screenpoint.Y - panoffsetY)) / (m_screenResolution * mapCtrl.Zoom);
            return new RPoint(xpos, ypos, 0);
        }
        //将屏幕距离计算为Zoom等级下的地图距离
        public static double ToUnit(float screenvalue, UcMapControl mapCtrl)
        {
            return (double)screenvalue / (double)(m_screenResolution * mapCtrl.Zoom);
        }
    }
}
