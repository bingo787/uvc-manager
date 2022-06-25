// ImageStatic.cpp : implementation file
//

#include "stdafx.h"
#include "LionSDKDemo.h"
#include "ImageStatic.h"


// CImageStatic

IMPLEMENT_DYNAMIC(CImageStatic, CStatic)

CImageStatic::CImageStatic()
{
	width = 0;
	height = 0;
	pImgData = 0;
}

CImageStatic::~CImageStatic()
{
	if(pImgData)
		free(pImgData);
	pImgData = 0;
}


BEGIN_MESSAGE_MAP(CImageStatic, CStatic)
	ON_WM_PAINT()
END_MESSAGE_MAP()



// CImageStatic message handlers







//设置图像内容
void CImageStatic::SetImageData(char* pData)
{
	memcpy(pImgData, pData, width*height*4);
	//
	Invalidate();
}
//设置图像信息
void CImageStatic::SetImageInfo(int w, int h)
{
	if(width != w && height != h)
	{
		width = w;
		height = h;

		if(pImgData)
			free(pImgData);
		pImgData = (char*)malloc(width*height*4);
	}
}


void CImageStatic::OnPaint()
{
	CPaintDC dc(this); // device context for painting
	// TODO: Add your message handler code here
	// Do not call CStatic::OnPaint() for painting messages
	CRect rect;
	GetClientRect(rect);

	if(pImgData)
	{
		BITMAPINFOHEADER bih = {0};       //位图信息头 
		bih.biBitCount = 32; //每个像素字节大小 
		bih.biHeight = 0 - height;
		bih.biCompression = BI_RGB;//BI_BITFIELDS;//BI_RGB;
		bih.biPlanes = 1; 
		bih.biSize = sizeof(BITMAPINFOHEADER); 
		bih.biSizeImage = width * height * 4;   //图像数据大小 
		bih.biWidth = width; 


		BITMAPFILEHEADER bfh = {0};   //位图文件头 
		bfh.bfOffBits = sizeof(BITMAPINFOHEADER) + sizeof(BITMAPFILEHEADER);  //到位图数据的偏移量 
		bfh.bfSize = bfh.bfOffBits + width * height * 4;   //文件总大小 
		bfh.bfType = (WORD)0x4d42;  // ((WORD) ('M' << 8) | 'B');  //'BM'    BITMAPINFOHEADER

		//
		BITMAPINFO bmpInfo = { 0 };
		memcpy(&bmpInfo.bmiHeader, &bih, sizeof(bih));
		////HBITMAP hBmp = CreateBitmap(1660, 2280, 1, 32, pImgData);
		HDC hMemDC = CreateCompatibleDC(dc.m_hDC);
		HBITMAP hMemMap = CreateCompatibleBitmap(hMemDC, width, height);
		SelectObject(hMemDC, hMemMap);
		//
		CBrush brush;
		brush.CreateSolidBrush(RGB(255, 255, 255));
		FillRect(hMemDC, rect, brush);

		//HBITMAP hBmp = CreateDIBSection(dc.m_hDC, &bmpInfo, DIB_RGB_COLORS,(void**)&pImgData, NULL, 0);
		//SelectObject(hMemDC, hBmp);
		//StretchBlt(dc.m_hDC, 0, 0, rect.Width(), rect.Height(), hMemDC, 0, 0, width, height, SRCCOPY);

		::StretchDIBits(dc.m_hDC, 0, 0, rect.Width(), rect.Height(), 0, 0, width, height, pImgData, &bmpInfo, DIB_RGB_COLORS, SRCCOPY);
		
		//StretchBlt(dc.m_hDC, 0, 0, rect.Width(), rect.Height(), hMemDC, 0, 0, width, height, SRCCOPY);
	}
	else
	{
		CBrush brush;
		brush.CreateSolidBrush(RGB(255, 255, 255));
		dc.FillRect(rect, &brush);
	}
}
