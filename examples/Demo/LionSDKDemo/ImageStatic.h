#pragma once


// CImageStatic

class CImageStatic : public CStatic
{
	DECLARE_DYNAMIC(CImageStatic)

public:
	CImageStatic();
	virtual ~CImageStatic();

protected:
	DECLARE_MESSAGE_MAP()


public:
	//设置图像内容
	void SetImageData(char* pData);
	//设置图像信息
	void SetImageInfo(int w, int h);

private:
	int		width;
	int		height;
	char*	pImgData;

public:
	afx_msg void OnPaint();
};


