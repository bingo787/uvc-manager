#ifndef __LION_COM_DATA_H__
#define __LION_COM_DATA_H__
#pragma once
//#include <afx.h>
//#include <comutil.h>
//#include <windows.h>
#include <vector>
#include <string>

using namespace std;



//
typedef int									LUVC_RESULT;

#define LUVC_SUCCESS						0x00					//成功
#define LUVC_FAIL							-0x01					//失败
#define LUVC_OPENED							-0x02					//设备已打开
#define LUVC_OTHERDEVOPENED					-0x03					//其他设备已打开
#define LUVC_UNOPEN							-0x04					//设备未打开

#define LUVC_PARAMINVALID					-0x10				//参数无效	
#define LUVC_WRITEFAILED					-0x11				//写入失败
#define LUVC_NOTSUPPORT						-0x20				//不支持
#define LUVC_MEMLOW							-0x30				//存储空间不足
#define LUVC_OVERTIME						-0x40				//超时
#define LUVC_TRIGGEROVERTIME				-0x41				//触发超时
#define LUVC_READIMAGEOVERTIME				-0x42				//读图超时


//Lion设备结构
typedef struct{
	unsigned int	Id;					//设备ID
	char			Name[130];				//设备名称
	char			DevSerial[66];			//设备序列号
	unsigned short	MajorNum;			//固件版本高位
	unsigned short	MinorNum;			//固件版本低位
}LionUVCDev, *PLionUVCDev;


//出图模式
enum LUVCDEV_MODE{
	LUVCDEVMODE_AC = 0,			//AC
	LUVCDEVMODE_VTC = 1,			//VTC
};

//BINNING
enum LUVCDEV_BINNING{
	LUVCDEVBINNING_NO = 0,		//NO
	LUVCDEVBINNING_YES = 1,		//Yes
};

//FPGA Filter
enum LUVCDEV_FILTER{
	LUVCDEVFILTER_NO = 0,			//No
	LUVCDEVFILTER_YES = 1,		//Yes
};

//X-RAY
enum LUVCDEV_XRAY
{
	LUVCDEVXRAY_VTCD = 0,		//VTC_D
	LUVCDEVXRAY_VTCA = 1,		//VTC_A
};

//图片格式
enum LUVCIMG_TYPE{
	LUVCIMG_RAW = 1,			//raw
	LUVCIMG_BMP,				//bmp
	LUVCIMG_JPEG,				//jpeg
	LUVCIMG_PNG,				//png
};

//设备状态
enum LUVCDEV_STATE{
	LUVCDEVSTATE_UNOPNE = 1,
	LUVCDEVSTATE_OPEN,
	LUVCDEVSTATE_WAITTRIGGER,
	LUVCDEVSTATE_TRIGGERIMAGE,
	LUVCDEVSTATE_IMAGESAVE,
	LUVCDEVSTATE_OVERTIME,
};
//#define LUVC_STATE_UNOPEN					-0x100				//设备未打开
//#define LUVC_STATE_OPEN						-0x101				//设备已打开
//#define LUVC_STATE_WAITTRIGGER				-0x102				//等待设备触发
//#define LUVC_STATE_TRIGGERIMG				-0x103				//设备触发传图
//#define LUVC_STATE_IMAGESAVE				-0x104				//图像保存


//设备参数
enum LUVCDEV_PARAM{
	LUVCDEVPARAM_MODE = 1,						//出图模式
	LUVCDEVPARAM_BINNING,						//BINNING
	LUVCDEVPARAM_FILTER,						//FPGA进行坏点过滤
	LUVCDEVPARAM_XRAY,
	LUVCDEVPARAM_IMAGE,							//图片格式	
	LUVCDEVPARAM_DELAYTIME,				     //延时设置 ms
	LUVCDEVPARAM_TRIGGERTIME,				//触发超时设置 s单位  AC=5s VTC=60S
	LUVCDEVPARAM_READIMAGETIME,				//读图超时设置 s秒	  5s
	LUVCDEVPARAM_XRESOLUTION,					
	LUVCDEVPARAM_YRESOLUTION,
	LUVCDEVPARAM_IMGWIDTH,					//图片宽度
	LUVCDEVPARAM_IMGHEIGHT,					//图片高宽
	LUVCDEVPARAM_SAMPLESPERPIXEL,
	LUVCDEVPARAM_BITESPERPIXEL,
	LUVCDEVPARAM_BITSPERSAMPLE,
	LUVCDEVPARAM_PLANAR,
	LUVCDEVPARAM_PIXELTYPE,
	LUVCDEVPARAM_COMPRESSION,
	LUVCDEVPARAM_BRIGHTNESS,					//亮度	0 - 100
	LUVCDEVPARAM_CONTRAST,						//对比度	0 - 100
	LUVCDEVPARAM_SATURATION,					//饱和度	0 - 100
	LUVCDEVPARAM_SHARPEN,						//锐化		0 - 100
	LUVCDEVPARAM_EMBOSS,						//浮雕		0 - 100
	LUVCDEVPARAM_BLUR,							//模糊    0 - 100
	LUVCDEVPARAM_GAMMA,						//直方图		0 - 100
	LUVCDEVPARAM_FFCDARKENABLE,				//暗场FFC校准使能
	LUVCDEVPARAM_FFCLIGHTENABLE,			//明场FFC校准使能
	LUVCDEVPARAM_FCCDARK,						//暗场FFC校准     文件路径
	LUVCDEVPARAM_FCCLIGHT,						//明场FFC校准		文件路径
	LUVCDEVPARAM_ACT,							//ACT  1.6版本新添加
};

//......触发位 
//			 ......获取可以读图
//							  ..读取图..
//										...图像保存...//
//设备已打开,设备未打开，获取图像(等待触发，触发传图，图像保存)
//open unopne 



#endif
