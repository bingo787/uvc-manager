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

#define LUVC_SUCCESS						0x00					//�ɹ�
#define LUVC_FAIL							-0x01					//ʧ��
#define LUVC_OPENED							-0x02					//�豸�Ѵ�
#define LUVC_OTHERDEVOPENED					-0x03					//�����豸�Ѵ�
#define LUVC_UNOPEN							-0x04					//�豸δ��

#define LUVC_PARAMINVALID					-0x10				//������Ч	
#define LUVC_WRITEFAILED					-0x11				//д��ʧ��
#define LUVC_NOTSUPPORT						-0x20				//��֧��
#define LUVC_MEMLOW							-0x30				//�洢�ռ䲻��
#define LUVC_OVERTIME						-0x40				//��ʱ
#define LUVC_TRIGGEROVERTIME				-0x41				//������ʱ
#define LUVC_READIMAGEOVERTIME				-0x42				//��ͼ��ʱ


//Lion�豸�ṹ
typedef struct{
	unsigned int	Id;					//�豸ID
	char			Name[130];				//�豸����
	char			DevSerial[66];			//�豸���к�
	unsigned short	MajorNum;			//�̼��汾��λ
	unsigned short	MinorNum;			//�̼��汾��λ
}LionUVCDev, *PLionUVCDev;


//��ͼģʽ
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

//ͼƬ��ʽ
enum LUVCIMG_TYPE{
	LUVCIMG_RAW = 1,			//raw
	LUVCIMG_BMP,				//bmp
	LUVCIMG_JPEG,				//jpeg
	LUVCIMG_PNG,				//png
};

//�豸״̬
enum LUVCDEV_STATE{
	LUVCDEVSTATE_UNOPNE = 1,
	LUVCDEVSTATE_OPEN,
	LUVCDEVSTATE_WAITTRIGGER,
	LUVCDEVSTATE_TRIGGERIMAGE,
	LUVCDEVSTATE_IMAGESAVE,
	LUVCDEVSTATE_OVERTIME,
};
//#define LUVC_STATE_UNOPEN					-0x100				//�豸δ��
//#define LUVC_STATE_OPEN						-0x101				//�豸�Ѵ�
//#define LUVC_STATE_WAITTRIGGER				-0x102				//�ȴ��豸����
//#define LUVC_STATE_TRIGGERIMG				-0x103				//�豸������ͼ
//#define LUVC_STATE_IMAGESAVE				-0x104				//ͼ�񱣴�


//�豸����
enum LUVCDEV_PARAM{
	LUVCDEVPARAM_MODE = 1,						//��ͼģʽ
	LUVCDEVPARAM_BINNING,						//BINNING
	LUVCDEVPARAM_FILTER,						//FPGA���л������
	LUVCDEVPARAM_XRAY,
	LUVCDEVPARAM_IMAGE,							//ͼƬ��ʽ	
	LUVCDEVPARAM_DELAYTIME,				     //��ʱ���� ms
	LUVCDEVPARAM_TRIGGERTIME,				//������ʱ���� s��λ  AC=5s VTC=60S
	LUVCDEVPARAM_READIMAGETIME,				//��ͼ��ʱ���� s��	  5s
	LUVCDEVPARAM_XRESOLUTION,					
	LUVCDEVPARAM_YRESOLUTION,
	LUVCDEVPARAM_IMGWIDTH,					//ͼƬ���
	LUVCDEVPARAM_IMGHEIGHT,					//ͼƬ�߿�
	LUVCDEVPARAM_SAMPLESPERPIXEL,
	LUVCDEVPARAM_BITESPERPIXEL,
	LUVCDEVPARAM_BITSPERSAMPLE,
	LUVCDEVPARAM_PLANAR,
	LUVCDEVPARAM_PIXELTYPE,
	LUVCDEVPARAM_COMPRESSION,
	LUVCDEVPARAM_BRIGHTNESS,					//����	0 - 100
	LUVCDEVPARAM_CONTRAST,						//�Աȶ�	0 - 100
	LUVCDEVPARAM_SATURATION,					//���Ͷ�	0 - 100
	LUVCDEVPARAM_SHARPEN,						//��		0 - 100
	LUVCDEVPARAM_EMBOSS,						//����		0 - 100
	LUVCDEVPARAM_BLUR,							//ģ��    0 - 100
	LUVCDEVPARAM_GAMMA,						//ֱ��ͼ		0 - 100
	LUVCDEVPARAM_FFCDARKENABLE,				//����FFCУ׼ʹ��
	LUVCDEVPARAM_FFCLIGHTENABLE,			//����FFCУ׼ʹ��
	LUVCDEVPARAM_FCCDARK,						//����FFCУ׼     �ļ�·��
	LUVCDEVPARAM_FCCLIGHT,						//����FFCУ׼		�ļ�·��
	LUVCDEVPARAM_ACT,							//ACT  1.6�汾�����
};

//......����λ 
//			 ......��ȡ���Զ�ͼ
//							  ..��ȡͼ..
//										...ͼ�񱣴�...//
//�豸�Ѵ�,�豸δ�򿪣���ȡͼ��(�ȴ�������������ͼ��ͼ�񱣴�)
//open unopne 



#endif
