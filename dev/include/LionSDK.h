// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the LIONSDK_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// LIONSDK_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef LIONSDK_EXPORTS
#define LIONSDK_API __declspec(dllexport)
#else
#define LIONSDK_API __declspec(dllimport)
#endif
#pragma once
#include <vector>

using namespace std;

//////
//////
typedef unsigned short	LU_UINT16;
typedef unsigned int	LU_UINT32;
typedef char			LU_STR4[4],			*PTW_STR4;
typedef char			LU_STR32[34],		*PTW_STR32; 
typedef char			LU_STR64[66],		*PTW_STR64;
typedef char			LU_STR128[130],		*PTW_STR128;
typedef char			LU_STR255[256],		*PTW_STR255;
typedef char*			LU_PCHAR;
typedef void*			LU_PVOID;

typedef int				LU_RESULT;


//��������
#define LUTY_INT8        0x0000
#define LUTY_INT16       0x0001
#define LUTY_INT32       0x0002

#define LUTY_UINT8       0x0003
#define LUTY_UINT16      0x0004
#define LUTY_UINT32      0x0005

#define LUTY_BOOL        0x0006

#define LUTY_FIX32       0x0007

#define LUTY_FRAME       0x0008

#define LUTY_STR32       0x0009
#define LUTY_STR64       0x000a
#define LUTY_STR128      0x000b
#define LUTY_STR255      0x000c
#define LUTY_HANDLE      0x000f

#define LUMF_APPOWNS     0x0101
#define LUMF_DSMOWNS     0x0102
#define LUMF_DSOWNS      0x0104
#define LUMF_POINTER     0x0108
#define LUMF_HANDLE      0x0110

#define LUON_ARRAY           0x0201
#define LUON_ENUMERATION     0x0202
#define LUON_ONEVALUE        0x0203
#define LUON_RANGE           0x0204

#define LUON_ICONID          0x0205
#define LUON_DSMID           0x0206
#define LUON_DSMCODEID       0x0207 

#define LUON_DONTCARE8       0x0208
#define LUON_DONTCARE16      0x0209
#define LUON_DONTCARE32      0x020A




#define LU_SUCCESS							0x00					//�ɹ�
#define LU_FAIL								-0x01					//ʧ��
#define LU_OPENED							-0x02					//�豸�Ѵ�
#define LU_OTHERDEVOPENED					-0x03					//�����豸�Ѵ�
#define LU_UNOPEN							-0x04					//�豸δ��

#define LU_PARAMINVALID						-0x10				//������Ч	
#define LU_WRITEFAILED						-0x11				//д��ʧ��
#define LU_NOTSUPPORT						-0x20				//��֧��
#define LU_MEMLOW							-0x30				//�洢�ռ䲻��
#define LU_OVERTIME							-0x40				//��ʱ
#define LU_TRIGGEROVERTIME					-0x41				//������ʱ
#define LU_READIMAGEOVERTIME				-0x42				//��ͼ��ʱ




//�豸״̬
enum LUDEV_STATE{
	LUDEVSTATE_UNOPNE = 1,
	LUDEVSTATE_OPEN,
	LUDEVSTATE_WAITTRIGGER,
	LUDEVSTATE_TRIGGERIMAGE,
	LUDEVSTATE_IMAGESAVE,
	LUDEVSTATE_OVERTIME,
};


//��ͼģʽ
enum LU_MODE{
	LUMODE_AC,			//AC
	LUMODE_UTC,			//UTC
};

//BINNING
enum LUDEV_BINNING{
	LUDEVBINNING_NO,		//NO
	LUDEVBINNING_YES,		//Yes
};

//FPGA Filter
enum LUDEV_FILTER{
	LUDEVFILTER_NO,			//No
	LUDEVFILTER_YES,		//Yes
};

//ͼƬ��ʽ
enum LUIMG_TYPE{
	LUIMG_RAW = 1,			//raw
	LUIMG_BMP,				//bmp
	LUIMG_JPEG,				//jpeg
	LUIMG_PNG,				//png
};

//�豸����
enum LUDEV_PARAM{
	LUDEVPARAM_UNKNOWN = 0,				    //
	LUDEVPARAM_MODE = 1,				    //��ͼģʽ
	LUDEVPARAM_BINNING,						//BINNING     //�ݲ�֧��
	LUDEVPARAM_FILTER,						//FPGA���л������
	LUDEVPARAM_XRAY,
	LUDEVPARAM_IMAGE,						//ͼƬ��ʽ    //�ݲ�֧��
	LUDEVPARAM_DELAYTIME,				    //�ӳ�ʱ�� ms��λ  //�ݲ�֧��
	LUDEVPARAM_TRIGGERTIME,				    //������ʱ���� s��λ  AC=5s VTC=60S
	LUDEVPARAM_READIMAGETIME,				//��ͼ��ʱ���� s��	  5s
	LUDEVPARAM_XRESOLUTION,					//�ݲ�֧��
	LUDEVPARAM_YRESOLUTION,                 //�ݲ�֧��
	LUDEVPARAM_IMGWIDTH,					//ͼƬ���      //�ݲ�֧��   
	LUDEVPARAM_IMGHEIGHT,					//ͼƬ�߿�      //�ݲ�֧��
	LUDEVPARAM_SAMPLESPERPIXEL,             //�ݲ�֧��
	LUDEVPARAM_BITESPERPIXEL,               //�ݲ�֧��
	LUDEVPARAM_BITSPERSAMPLE,               //�ݲ�֧��
	LUDEVPARAM_PLANAR,                      //�ݲ�֧��
	LUDEVPARAM_PIXELTYPE,                   //�ݲ�֧��
	LUDEVPARAM_COMPRESSION,                 //�ݲ�֧�� 
	LUDEVPARAM_BRIGHTNESS,					//����	0 - 100      //�ݲ�֧��
	LUDEVPARAM_CONTRAST,				    //�Աȶ�	0 - 100   //�ݲ�֧��
	LUDEVPARAM_SATURATION,					//���Ͷ�	0 - 100   //�ݲ�֧��
	LUDEVPARAM_SHARPEN,						//��		0 - 100   //�ݲ�֧��
	LUDEVPARAM_EMBOSS,						//����		0 - 100   //�ݲ�֧��
	LUDEVPARAM_BLUR,					    //ģ��    0 - 100      //�ݲ�֧��
	LUDEVPARAM_GAMMA,						//ֱ��ͼ		0 - 100    //�ݲ�֧��
	LUDEVPARAM_FFCDARKENABLE,				//����FFCУ׼ʹ��          //�ݲ�֧��
	LUDEVPARAM_FFCLIGHTENABLE,			    //����FFCУ׼ʹ��          //�ݲ�֧��
	LUDEVPARAM_FCCDARK,						//����FFCУ׼     �ļ�·��   //�ݲ�֧��
	LUDEVPARAM_FCCLIGHT,					//����FFCУ׼		�ļ�·��  //�ݲ�֧��
};

//TWPARAMETER  //�ݲ�֧��
enum TWDEV_PARAM
{
	TW_CAP_UNKONWN = 0,
	TW_CAP_CUSTOMBASE = 0x8000,					 /* Base of custom capabilities */
	/* all data sources are REQUIRED to support these caps */
	TW_CAP_XFERCOUNT = 0x0001,
	/* image data sources are REQUIRED to support these caps */
	TW_ICAP_COMPRESSION = 0x0100,
	TW_ICAP_PIXELTYPE = 0x0101,
	TW_ICAP_UNITS = 0x0102,
	TW_ICAP_XFERMECH = 0x0103,

	/* all data sources MAY support these caps */
	TW_CAP_AUTHOR = 0x1000,
	TW_CAP_CAPTION = 0x1001,
	TW_CAP_FEEDERENABLED = 0x1002,
	TW_CAP_FEEDERLOADED = 0x1003,
	TW_CAP_TIMEDATE = 0x1004,
	TW_CAP_SUPPORTEDCAPS = 0x1005,
	TW_CAP_EXTENDEDCAPS = 0x1006,
	TW_CAP_AUTOFEED = 0x1007,
	TW_CAP_CLEARPAGE = 0x1008,
	TW_CAP_FEEDPAGE = 0x1009,
	TW_CAP_REWINDPAGE = 0x100a,
	TW_CAP_INDICATORS = 0x100b,
	TW_CAP_PAPERDETECTABLE = 0x100d,
	TW_CAP_UICONTROLLABLE = 0x100e,
	TW_CAP_DEVICEONLINE = 0x100f,
	TW_CAP_AUTOSCAN = 0x1010,
	TW_CAP_THUMBNAILSENABLED = 0x1011,
	TW_CAP_DUPLEX = 0x1012,
	TW_CAP_DUPLEXENABLED = 0x1013,
	TW_CAP_ENABLEDSUIONLY = 0x1014,
	TW_CAP_CUSTOMDSDATA = 0x1015,
	TW_CAP_ENDORSER = 0x1016,
	TW_CAP_JOBCONTROL = 0x1017,
	TW_CAP_ALARMS = 0x1018,
	TW_CAP_ALARMVOLUME = 0x1019,
	TW_CAP_AUTOMATICCAPTURE = 0x101a,
	TW_CAP_TIMEBEFOREFIRSTCAPTURE = 0x101b,
	TW_CAP_TIMEBETWEENCAPTURES = 0x101c,
	TW_CAP_MAXBATCHBUFFERS = 0x101e,
	TW_CAP_DEVICETIMEDATE = 0x101f,
	TW_CAP_POWERSUPPLY = 0x1020,
	TW_CAP_CAMERAPREVIEWUI = 0x1021,
	TW_CAP_DEVICEEVENT = 0x1022,
	TW_CAP_SERIALNUMBER = 0x1024,
	TW_CAP_PRINTER = 0x1026,
	TW_CAP_PRINTERENABLED = 0x1027,
	TW_CAP_PRINTERINDEX = 0x1028,
	TW_CAP_PRINTERMODE = 0x1029,
	TW_CAP_PRINTERSTRING = 0x102a,
	TW_CAP_PRINTERSUFFIX = 0x102b,
	TW_CAP_LANGUAGE = 0x102c,
	TW_CAP_FEEDERALIGNMENT = 0x102d,
	TW_CAP_FEEDERORDER = 0x102e,
	TW_CAP_REACQUIREALLOWED = 0x1030,
	TW_CAP_BATTERYMINUTES = 0x1032,
	TW_CAP_BATTERYPERCENTAGE = 0x1033,
	TW_CAP_CAMERASIDE = 0x1034,
	TW_CAP_SEGMENTED = 0x1035,
	TW_CAP_CAMERAENABLED = 0x1036,
	TW_CAP_CAMERAORDER = 0x1037,
	TW_CAP_MICRENABLED = 0x1038,
	TW_CAP_FEEDERPREP = 0x1039,
	TW_CAP_FEEDERPOCKET = 0x103a,
	TW_CAP_AUTOMATICSENSEMEDIUM = 0x103b,
	TW_CAP_CUSTOMINTERFACEGUID = 0x103c,
	TW_CAP_SUPPORTEDCAPSSEGMENTUNIQUE = 0x103d,
	TW_CAP_SUPPORTEDDATS = 0x103e,
	TW_CAP_DOUBLEFEEDDETECTION = 0x103f,
	TW_CAP_DOUBLEFEEDDETECTIONLENGTH = 0x1040,
	TW_CAP_DOUBLEFEEDDETECTIONSENSITIVITY = 0x1041,
	TW_CAP_DOUBLEFEEDDETECTIONRESPONSE = 0x1042,
	TW_CAP_PAPERHANDLING = 0x1043,
	TW_CAP_INDICATORSMODE = 0x1044,
	TW_CAP_PRINTERVERTICALOFFSET = 0x1045,
	TW_CAP_POWERSAVETIME = 0x1046,
	TW_CAP_PRINTERCHARROTATION = 0x1047,
	TW_CAP_PRINTERFONTSTYLE = 0x1048,
	TW_CAP_PRINTERINDEXLEADCHAR = 0x1049,
	TW_CAP_PRINTERINDEXMAXVALUE = 0x104A,
	TW_CAP_PRINTERINDEXNUMDIGITS = 0x104B,
	TW_CAP_PRINTERINDEXSTEP = 0x104C,
	TW_CAP_PRINTERINDEXTRIGGER = 0x104D,
	TW_CAP_PRINTERSTRINGPREVIEW = 0x104E,
	TW_CAP_SHEETCOUNT = 0x104F,

	/* image data sources MAY support these caps */
	TW_ICAP_AUTOBRIGHT = 0x1100,
	TW_ICAP_BRIGHTNESS = 0x1101,
	TW_ICAP_CONTRAST = 0x1103,
	TW_ICAP_CUSTHALFTONE = 0x1104,
	TW_ICAP_EXPOSURETIME = 0x1105,
	TW_ICAP_FILTER = 0x1106,
	TW_ICAP_FLASHUSED = 0x1107,
	TW_ICAP_GAMMA = 0x1108,
	TW_ICAP_HALFTONES = 0x1109,
	TW_ICAP_HIGHLIGHT = 0x110a,
	TW_ICAP_IMAGEFILEFORMAT = 0x110c,
	TW_ICAP_LAMPSTATE = 0x110d,
	TW_ICAP_LIGHTSOURCE = 0x110e,
	TW_ICAP_ORIENTATION = 0x1110,
	TW_ICAP_PHYSICALWIDTH = 0x1111,
	TW_ICAP_PHYSICALHEIGHT = 0x1112,
	TW_ICAP_SHADOW = 0x1113,
	TW_ICAP_FRAMES = 0x1114,
	TW_ICAP_XNATIVERESOLUTION = 0x1116,
	TW_ICAP_YNATIVERESOLUTION = 0x1117,
	TW_ICAP_XRESOLUTION = 0x1118,
	TW_ICAP_YRESOLUTION = 0x1119,
	TW_ICAP_MAXFRAMES = 0x111a,
	TW_ICAP_TILES = 0x111b,
	TW_ICAP_BITORDER = 0x111c,
	TW_ICAP_CCITTKFACTOR = 0x111d,
	TW_ICAP_LIGHTPATH = 0x111e,
	TW_ICAP_PIXELFLAVOR =  0x111f,
	TW_ICAP_PLANARCHUNKY = 0x1120,
	TW_ICAP_ROTATION = 0x1121,
	TW_ICAP_SUPPORTEDSIZES = 0x1122,
	TW_ICAP_THRESHOLD = 0x1123,
	TW_ICAP_XSCALING = 0x1124,
	TW_ICAP_YSCALING = 0x1125,
	TW_ICAP_BITORDERCODES = 0x1126,
	TW_ICAP_PIXELFLAVORCODES = 0x1127,
	TW_ICAP_JPEGPIXELTYPE = 0x1128,
	TW_ICAP_TIMEFILL = 0x112a,
	TW_ICAP_BITDEPTH = 0x112b,
	TW_ICAP_BITDEPTHREDUCTION = 0x112c,
	TW_ICAP_UNDEFINEDIMAGESIZE = 0x112d,
	TW_ICAP_IMAGEDATASET = 0x112e,
	TW_ICAP_EXTIMAGEINFO = 0x112f,
	TW_ICAP_MINIMUMHEIGHT = 0x1130,
	TW_ICAP_MINIMUMWIDTH = 0x1131,
	TW_ICAP_AUTODISCARDBLANKPAGES = 0x1134,
	TW_ICAP_FLIPROTATION = 0x1136,
	TW_ICAP_BARCODEDETECTIONENABLED = 0x1137,
	TW_ICAP_SUPPORTEDBARCODETYPES = 0x1138,
	TW_ICAP_BARCODEMAXSEARCHPRIORITIES = 0x1139,
	TW_ICAP_BARCODESEARCHPRIORITIES = 0x113a,
	TW_ICAP_BARCODESEARCHMODE = 0x113b,
	TW_ICAP_BARCODEMAXRETRIES = 0x113c,
	TW_ICAP_BARCODETIMEOUT = 0x113d,
	TW_ICAP_ZOOMFACTOR = 0x113e,
	TW_ICAP_PATCHCODEDETECTIONENABLED = 0x113f,
	TW_ICAP_SUPPORTEDPATCHCODETYPES = 0x1140,
	TW_ICAP_PATCHCODEMAXSEARCHPRIORITIES = 0x1141,
	TW_ICAP_PATCHCODESEARCHPRIORITIES = 0x1142,
	TW_ICAP_PATCHCODESEARCHMODE = 0x1143,
	TW_ICAP_PATCHCODEMAXRETRIES = 0x1144,
	TW_ICAP_PATCHCODETIMEOUT = 0x1145,
	TW_ICAP_FLASHUSED2 = 0x1146,
	TW_ICAP_IMAGEFILTER = 0x1147,
	TW_ICAP_NOISEFILTER = 0x1148,
	TW_ICAP_OVERSCAN = 0x1149,
	TW_ICAP_AUTOMATICBORDERDETECTION = 0x1150,
	TW_ICAP_AUTOMATICDESKEW = 0x1151,
	TW_ICAP_AUTOMATICROTATE = 0x1152,
	TW_ICAP_JPEGQUALITY = 0x1153,
	TW_ICAP_FEEDERTYPE = 0x1154,
	TW_ICAP_ICCPROFILE = 0x1155,
	TW_ICAP_AUTOSIZE = 0x1156,
	TW_ICAP_AUTOMATICCROPUSESFRAME = 0x1157,
	TW_ICAP_AUTOMATICLENGTHDETECTION = 0x1158,
	TW_ICAP_AUTOMATICCOLORENABLED = 0x1159,
	TW_ICAP_AUTOMATICCOLORNONCOLORPIXELTYPE = 0x115a,
	TW_ICAP_COLORMANAGEMENTENABLED = 0x115b,
	TW_ICAP_IMAGEMERGE = 0x115c,
	TW_ICAP_IMAGEMERGEHEIGHTTHRESHOLD = 0x115d,
	TW_ICAP_SUPPORTEDEXTIMAGEINFO = 0x115e,
	TW_ICAP_FILMTYPE = 0x115f,
	TW_ICAP_MIRROR = 0x1160,
	TW_ICAP_JPEGSUBSAMPLING = 0x1161,

	/* image data sources MAY support these audio caps */
	TW_ACAP_XFERMECH = 0x1202,
};


typedef struct {
   LU_UINT16  MajorNum;
   LU_UINT16  MinorNum;
   LU_UINT16  Language;
   LU_UINT16  Country;
   LU_STR32	  Info;
} LU_VERSION, FAR * PLU_VERSION;


/////
typedef struct {
	LU_UINT32		Id;
    LU_VERSION 		Version;
    LU_UINT16  		ProtocolMajor;
    LU_UINT16  		ProtocolMinor;
    LU_UINT32  		SupportedGroups;
    LU_STR32   		Manufacturer;
    LU_STR32   		ProductFamily;
    LU_STR32   		ProductName;
} TWD_IDENTITY, *PTWD_IDENTITY;


//
typedef struct{
	LU_UINT32	Id;					//�豸ID
	LU_STR128	Name;				//�豸����
	LU_STR64	DevSerial;			//�豸���к�
	LU_UINT16	MajorNum;			//�̼��汾��λ
	LU_UINT16	MinorNum;			//�̼��汾��λ
}LU_IDENTITY, *PLU_IDENTITY;


////
typedef struct {
	LU_IDENTITY uvcIdentity;
	TWD_IDENTITY twIdentity;
}LU_DEVICE, *PLU_DEVICE;

//����
typedef struct
{
	LU_UINT16 param;		//���� ,����ʽ������ʽ�����ڼ���
	LU_UINT16 type;			//�洢�ռ���������, dataʵ����������Ϊint, short, char.....
	LU_UINT32 size;			//�洢�ռ��С,byte����
	LU_PVOID  data;			//�洢�ռ䣬���ڴ洢ʵ�ʲ���ֵ
}LU_PARAM, *PLU_PARAM;



/*********************************************************************************************************************
 * @brief ��ȡ�豸����
 *
 * @param[in] enumLion ö���豸���ͣ�1: uvc, 0: twain
 * @return �����豸����
*********************************************************************************************************************/
LIONSDK_API LU_RESULT GetDeviceCount(LU_UINT32 enumLion = 1);
/*********************************************************************************************************************
 * @brief ��ȡ�豸
 * 
 * @param[in] index �豸���� 0, 1, 2, 3....
 * @param[in] enumLion ö���豸���ͣ�1: uvc, 0: twain
 * @return �����豸
*********************************************************************************************************************/
LIONSDK_API PLU_DEVICE GetDevice(LU_UINT32 index, LU_UINT32 enumLion = 1);

/*********************************************************************************************************************
 * @brief ���豸
 *
 * @param[in] pDevice��Ҫ�򿪵��豸·��
 * @return LU_SUCCESS���ɹ���������ʧ��
*********************************************************************************************************************/
LIONSDK_API LU_RESULT OpenDevice(PLU_DEVICE pDevice);

/*********************************************************************************************************************
 * @brief �ر��豸
 *
 * @param[in] pDevice��Ҫ�رյ��豸·��
 * @return LU_SUCCESS���ɹ���������ʧ��
*********************************************************************************************************************/
LIONSDK_API LU_RESULT CloseDevice(PLU_DEVICE pDevice);


/*********************************************************************************************************************
 * @brief ��ѯ�豸״̬��ֻ����uvc�豸
 *
 * @param[in] pDevice��Ҫ��ѯ���豸·��
 * @param[out] devState ���ص��豸״̬
 * @return LU_SUCCESS���ɹ���������ʧ��
*********************************************************************************************************************/
LIONSDK_API LU_RESULT GetDeviceState(PLU_DEVICE pDevice, LUDEV_STATE* devState);

/*********************************************************************************************************************
 * @brief �����豸���к�, ֻ��UVC�豸��Ч
 *
 * @param[in,out] pDevice��Ҫ���õ��豸,�ɹ��������ú�����к��豸
 * @param[in] pSerial ����д������к�
 * @return LU_SUCCESS���ɹ���������ʧ��
*********************************************************************************************************************/
LIONSDK_API LU_RESULT SetDeviceSerial(PLU_DEVICE pDevice, char* pSerial);



/*********************************************************************************************************************
 * @brief ���ò���
 *
 * @param[in] cDev��Ҫ�򿪵��豸·��
 * @param[in, out] param �����õĲ����ṹ
 * @return LU_SUCCESS���ɹ���������ʧ��
*********************************************************************************************************************/
LIONSDK_API LU_RESULT SetDeviceParam(PLU_DEVICE pDevice, PLU_PARAM pParam);

/*********************************************************************************************************************
 * @brief ��ȡ����
 *
 * @param[in] cDev��Ҫ�򿪵��豸·��
 * @param[in, out] param ����ȡ�Ĳ����ṹ
 * @return LU_SUCCESS���ɹ���������ʧ��
*********************************************************************************************************************/
LIONSDK_API	LU_RESULT GetDeviceParam(PLU_DEVICE pDevice, PLU_PARAM pParam);

/*********************************************************************************************************************
 * @brief ��ȡͼ��
 *
 * @param[in] cDev��Ҫ�򿪵��豸·��
 * @param[in] showUi�Ƿ���ʾUI;  1:��ʾ����������ʾ
 * @param[in,out] pImgDataͼ������
 * @param[in,out] nDataBufͼ�����ݵĴ�С
 * @param[in,out] pFileͼ�񱣴���ļ�λ��
 * @param[in,out] nFileBufͼ���ļ�·����С
 * @return LU_SUCCESS���ɹ���������ʧ��
*********************************************************************************************************************/
LIONSDK_API LU_RESULT GetImage(PLU_DEVICE pDevice, LU_UINT32 showUi, LU_PCHAR pImgData, LU_UINT32* nDataBuf, LU_PCHAR pFile, LU_UINT32* nFileBuf);



/*********************************************************************************************************************
 * @brief �첽��ȡͼ��
 *
 * @param[in] device��Ҫ�򿪵��豸
 * @param[in] showUi�Ƿ���ʾUI;  1:��ʾ����������ʾ
 * @param[in] callback���غ���
 * @return LU_SUCCESS���ɹ���������ʧ��
*********************************************************************************************************************/
//���غ�������
typedef LU_RESULT (WINAPI *PLionImageCallback)(LU_DEVICE device, LU_PCHAR pImgData, LU_UINT32 nDataBuf, LU_PCHAR pFile, LU_UINT32 nFileBuf);

LIONSDK_API LU_RESULT GetImage(PLU_DEVICE pDevice, LU_UINT32 showUi, PLionImageCallback callback);

/*********************************************************************************************************************
 * @brief ����ڻ�ȡͼ���������ȡ
 *
 * @param[in] cDev��Ҫ�򿪵��豸·��
 * @return LU_SUCCESS���ɹ���������ʧ��
*********************************************************************************************************************/
LIONSDK_API LU_RESULT AbandonGetImage(PLU_DEVICE pDevice);




