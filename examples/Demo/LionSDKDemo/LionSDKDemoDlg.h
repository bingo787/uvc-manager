
// LionSDKDemoDlg.h : header file
//

#pragma once
#include "afxwin.h"
#include "afxcmn.h"
#include "LionSDK.h"
#include "ImageStatic.h"


// CLionSDKDemoDlg dialog
class CLionSDKDemoDlg : public CDialogEx
{
// Construction
public:
	CLionSDKDemoDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_LIONSDKDEMO_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnNMClickTreeDevice(NMHDR *pNMHDR, LRESULT *pResult);
	afx_msg void OnBnClickedButtonOpen();
	afx_msg void OnBnClickedButtonUi();
	afx_msg void OnBnClickedButtonEnum();
	afx_msg void OnBnClickedButtonQuit();
	afx_msg void OnBnClickedButtonClose();

	afx_msg void OnBnClickedButtonModifySerial();

	afx_msg void OnEnChangeEditBrightness();
	afx_msg void OnEnChangeEditContrast();
	afx_msg void OnEnChangeEditSaturation();
	afx_msg void OnEnChangeEditSharpen();
	afx_msg void OnEnChangeEditEmboss();
	afx_msg void OnEnChangeEditBlur();
	afx_msg void OnEnChangeEditGamma();

	afx_msg void OnHScroll(UINT nSBCode, UINT nPos, CScrollBar* pScrollBar);
public:
	static CLionSDKDemoDlg  *pSDKDemoDlg;
	static LU_RESULT WINAPI ImageCallback(LU_DEVICE device, LU_PCHAR pImgData, LU_UINT32 nDataBuf, LU_PCHAR pFile, LU_UINT32 nFileBuf);
	//同步获取图像线程
	static DWORD WINAPI GetImageThreadProc(LPVOID lpParam);
	//获取图像
	void GetSynchronousImage();

	//批量异步操作获取图像线程
	static DWORD WINAPI AsynchronousGetImageThreadProc(LPVOID lpParam);
	//获取图像
	void AsynchronousGetImage();
public:
	//
	void ClearTreeState();
	void ClearTreeNodeState(HTREEITEM hItem);
	//
	LU_RESULT ShowImageData(LU_DEVICE device, LU_PCHAR pImgData, LU_UINT32 nDataBuf, LU_PCHAR pFile, LU_UINT32 nFileBuf);

	//显示设备信息
	void ShowDeviceInfo(PLU_DEVICE pDevice);
	//设备图像参数
	void SetImageDataParam(int nParam, int nValue);
private:
	CImageStatic m_stImage;
	CTreeCtrl m_treeDevice;
	CButton m_btnOpen;
	CButton m_btnUI;
	CButton m_btnEnum;
	CButton m_btnGetImage;
	CButton m_btnQuit;
	CButton m_btnClose;
	//参数设置控件
	CEdit				m_etSerial;
	CButton				m_btnModifySerial;
	CEdit				m_etBrightness;
	CSliderCtrl			m_scBrightness;
	CEdit				m_etContrast;
	CSliderCtrl			m_scContrast;
	CEdit				m_etSaturation;
	CSliderCtrl			m_scSaturation;
	CEdit				m_etSHarpen;
	CSliderCtrl			m_scSharpen;
	CEdit				m_etEmboss;
	CSliderCtrl			m_scEmboss;
	CEdit				m_etBlur;
	CSliderCtrl			m_scBlur;
	CEdit				m_etGamma;
	CSliderCtrl			m_scGamma;
private:
	vector<LU_DEVICE>     m_vcUVCDevice;
	vector<LU_DEVICE>	  m_vcTwainDevice;

	//
	//异步测试线程句柄
	HANDLE				m_hAsyThread;
	//异步同步句柄
	HANDLE				m_hAsyEvent;

	//返回图像文件名称长度
	int					m_nFile;
	//返回图像文件名称
	char				m_cFile[512];

	//同步测试线程句柄
	HANDLE				m_hSynThread;

public:
	CStatic m_stDeviceInfo;
	afx_msg void OnBnClickedButtonSynchronous();
	afx_msg void OnBnClickedButtonAsynchronous();
	afx_msg void OnBnClickedButtonGetstate();
	afx_msg void OnBnClickedButtonAbandonimg();
	CStatic m_stStateInfo;
	CButton m_btnSynchronous;
	CButton m_btnAsynchronous;
	afx_msg void OnBnClickedButtonModifyModel();
	CComboBox m_cbModelIMg;
	CComboBox m_cbBinning;
	CComboBox m_cbModelFilter;
	CComboBox m_cbModelX_Ray;
	CEdit m_etCheckTime;
	CEdit m_etGetTime;
	CButton m_btnAbandon;
};
