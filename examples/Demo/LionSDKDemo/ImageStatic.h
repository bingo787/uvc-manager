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
	//����ͼ������
	void SetImageData(char* pData);
	//����ͼ����Ϣ
	void SetImageInfo(int w, int h);

private:
	int		width;
	int		height;
	char*	pImgData;

public:
	afx_msg void OnPaint();
};


