
// LionSDKDemoDlg.cpp : implementation file
//

#include "stdafx.h"
#include "LionSDKDemo.h"
#include "LionSDKDemoDlg.h"
#include "afxdialogex.h"

//#define _CRTDBG_MAP_ALLOC
//#include <stdlib.h>
//#include <crtdbg.h>


#ifdef _DEBUG
#define new DEBUG_NEW
#endif

#ifdef WIN64
#ifdef _DEBUG
#pragma	comment(lib,"../public/lib/debug/64/LionSDK.lib")
#else
#pragma	comment(lib,"../public/lib/release/64/LionSDK.lib")
#endif
#else
#ifdef _DEBUG
#pragma	comment(lib,"../public/lib/debug/32/LionSDK.lib")
#else
#pragma	comment(lib,"../public/lib/release/32/LionSDK.lib")
#endif
#endif
// CAboutDlg dialog used for App About

class CAboutDlg : public CDialogEx
{
public:
	CAboutDlg();

// Dialog Data
	enum { IDD = IDD_ABOUTBOX };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

// Implementation
protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialogEx(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialogEx)
END_MESSAGE_MAP()


// CLionSDKDemoDlg dialog


CLionSDKDemoDlg*  CLionSDKDemoDlg::pSDKDemoDlg = 0;

CLionSDKDemoDlg::CLionSDKDemoDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CLionSDKDemoDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	pSDKDemoDlg = this;
	//
	//异步测试线程句柄
	m_hAsyThread = NULL;
	m_hAsyEvent = NULL;
	//同步测试线程句柄
	m_hSynThread = NULL;
}

void CLionSDKDemoDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_STATIC_IMAGE, m_stImage);
	DDX_Control(pDX, IDC_TREE_DEVICE, m_treeDevice);
	DDX_Control(pDX, IDC_BUTTON_OPEN, m_btnOpen);
	DDX_Control(pDX, IDC_BUTTON_ENUM, m_btnEnum);
	DDX_Control(pDX, ID_BUTTON_QUIT, m_btnQuit);
	DDX_Control(pDX, IDC_BUTTON_CLOSE, m_btnClose);
	DDX_Control(pDX, IDC_BUTTON_MODIFY_SERIAL, m_btnModifySerial);
	DDX_Control(pDX, IDC_EDIT_SERIAL, m_etSerial);

	DDX_Control(pDX, IDC_EDIT_BRIGHTNESS, m_etBrightness);
	DDX_Control(pDX, IDC_SLIDER_BRIGHTNESS, m_scBrightness);
	DDX_Control(pDX, IDC_EDIT_CONTRAST, m_etContrast);
	DDX_Control(pDX, IDC_SLIDER_CONTRAST, m_scContrast);
	DDX_Control(pDX, IDC_EDIT_SATURATION, m_etSaturation);
	DDX_Control(pDX, IDC_SLIDER_SATURATION, m_scSaturation);
	DDX_Control(pDX, IDC_EDIT_SHARPEN, m_etSHarpen);
	DDX_Control(pDX, IDC_SLIDER_SHARPEN, m_scSharpen);
	DDX_Control(pDX, IDC_EDIT_EMBOSS, m_etEmboss);
	DDX_Control(pDX, IDC_SLIDER_EMBOSS, m_scEmboss);
	DDX_Control(pDX, IDC_EDIT_BLUR, m_etBlur);
	DDX_Control(pDX, IDC_SLIDER_BLUR, m_scBlur);
	DDX_Control(pDX, IDC_EDIT_GAMMA, m_etGamma);
	DDX_Control(pDX, IDC_SLIDER_GAMMA, m_scGamma);
	DDX_Control(pDX, IDC_STATIC_DEVICE_INFO, m_stDeviceInfo);
	DDX_Control(pDX, IDC_STATIC_STATEINFO, m_stStateInfo);
	DDX_Control(pDX, ID_BUTTON_SYNCHRONOUS, m_btnSynchronous);
	DDX_Control(pDX, IDC_BUTTON_ASYNCHRONOUS, m_btnAsynchronous);
	DDX_Control(pDX, IDC_COMBO_MODEL_IMG, m_cbModelIMg);
	DDX_Control(pDX, IDC_COMBO_MODEL_BINNING, m_cbBinning);
	DDX_Control(pDX, IDC_COMBO_MODEL_FILTER, m_cbModelFilter);
	DDX_Control(pDX, IDC_COMBO_MODEL_X_RAY, m_cbModelX_Ray);
	DDX_Control(pDX, IDC_EDIT_CHECKTIME, m_etCheckTime);
	DDX_Control(pDX, IDC_EDIT_GETTIME, m_etGetTime);
	DDX_Control(pDX, IDC_BUTTON_ABANDONIMG, m_btnAbandon);
}

BEGIN_MESSAGE_MAP(CLionSDKDemoDlg, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_NOTIFY(NM_CLICK, IDC_TREE_DEVICE, &CLionSDKDemoDlg::OnNMClickTreeDevice)
	ON_BN_CLICKED(IDC_BUTTON_OPEN, &CLionSDKDemoDlg::OnBnClickedButtonOpen)
	ON_BN_CLICKED(IDC_BUTTON_ENUM, &CLionSDKDemoDlg::OnBnClickedButtonEnum)
	ON_BN_CLICKED(ID_BUTTON_QUIT, &CLionSDKDemoDlg::OnBnClickedButtonQuit)
	ON_BN_CLICKED(IDC_BUTTON_CLOSE, &CLionSDKDemoDlg::OnBnClickedButtonClose)
	ON_BN_CLICKED(IDC_BUTTON_MODIFY_SERIAL, &CLionSDKDemoDlg::OnBnClickedButtonModifySerial)

	ON_EN_CHANGE(IDC_EDIT_BRIGHTNESS, &CLionSDKDemoDlg::OnEnChangeEditBrightness)

	ON_WM_HSCROLL()
	ON_EN_CHANGE(IDC_EDIT_CONTRAST, &CLionSDKDemoDlg::OnEnChangeEditContrast)
	ON_EN_CHANGE(IDC_EDIT_SATURATION, &CLionSDKDemoDlg::OnEnChangeEditSaturation)
	ON_EN_CHANGE(IDC_EDIT_SHARPEN, &CLionSDKDemoDlg::OnEnChangeEditSharpen)
	ON_EN_CHANGE(IDC_EDIT_EMBOSS, &CLionSDKDemoDlg::OnEnChangeEditEmboss)
	ON_EN_CHANGE(IDC_EDIT_BLUR, &CLionSDKDemoDlg::OnEnChangeEditBlur)
	ON_EN_CHANGE(IDC_EDIT_GAMMA, &CLionSDKDemoDlg::OnEnChangeEditGamma)

	ON_BN_CLICKED(ID_BUTTON_SYNCHRONOUS, &CLionSDKDemoDlg::OnBnClickedButtonSynchronous)
	ON_BN_CLICKED(IDC_BUTTON_ASYNCHRONOUS, &CLionSDKDemoDlg::OnBnClickedButtonAsynchronous)
	ON_BN_CLICKED(IDC_BUTTON_GETSTATE, &CLionSDKDemoDlg::OnBnClickedButtonGetstate)
	ON_BN_CLICKED(IDC_BUTTON_ABANDONIMG, &CLionSDKDemoDlg::OnBnClickedButtonAbandonimg)
	ON_BN_CLICKED(IDC_BUTTON_MODIFY_MODEL, &CLionSDKDemoDlg::OnBnClickedButtonModifyModel)
END_MESSAGE_MAP()


// CLionSDKDemoDlg message handlers

BOOL CLionSDKDemoDlg::OnInitDialog()
{
	
	CDialogEx::OnInitDialog();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		BOOL bNameValid;
		CString strAboutMenu;
		bNameValid = strAboutMenu.LoadString(IDS_ABOUTBOX);
		ASSERT(bNameValid);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	// TODO: Add extra initialization here

	m_btnOpen.EnableWindow(false);
	m_btnClose.EnableWindow(false);



	m_cbModelIMg.Clear();
	m_cbModelIMg.InsertString(0, _T("AC 出图"));
	m_cbModelIMg.InsertString(1, _T("VTC 出图"));
	m_cbModelIMg.SetCurSel(0);

	m_cbBinning.Clear();
	m_cbBinning.InsertString(0, _T("No Binning"));
	m_cbBinning.InsertString(1, _T("Binning"));
	m_cbBinning.SetCurSel(0);
	
	m_cbModelFilter.Clear();
	m_cbModelFilter.InsertString(0, _T("原始图像"));
	m_cbModelFilter.InsertString(1, _T("FPGA 进行坏点过滤"));
	m_cbModelFilter.SetCurSel(0);
	
	m_cbModelX_Ray.Clear();
	m_cbModelX_Ray.InsertString(0, _T("VTC-D"));
	m_cbModelX_Ray.InsertString(1, _T("VTC-A"));
	m_cbModelX_Ray.SetCurSel(0);

	//检测时间
	m_etCheckTime.SetWindowText(_T("5000"));
	//出图时间
	m_etGetTime.SetWindowText(_T("10000"));

	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CLionSDKDemoDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialogEx::OnSysCommand(nID, lParam);
	}
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CLionSDKDemoDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CLionSDKDemoDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}



void CLionSDKDemoDlg::OnNMClickTreeDevice(NMHDR *pNMHDR, LRESULT *pResult)
{
	// TODO: Add your control notification handler code here
	//
	CPoint pt;
	GetCursorPos(&pt);
	m_treeDevice.ScreenToClient(&pt); 
	UINT nFlag = 0; 
	HTREEITEM hItem = m_treeDevice.HitTest(pt, &nFlag);
	if(NULL == hItem)    
	{
		//m_treeDevice.SetCheck(hItem, FALSE);
		return;
	}
	//if(m_treeDevice.GetParentItem(hItem) == NULL)
	//{
	//	m_treeDevice.SetCheck(hItem, FALSE);
	//	return;
	//}
	//
	if(nFlag & TVHT_ONITEM){
		//清空选择项
		ClearTreeState();
		if(m_treeDevice.GetParentItem(hItem) == NULL || m_treeDevice.ItemHasChildren(hItem))
		{	
			//m_treeDevice.SetItemState(hItem, 0, TVIS_SELECTED);
			m_treeDevice.SetCheck(hItem, FALSE);
			*pResult = 1;
			return;
		}
		m_treeDevice.SetCheck(hItem, TRUE);
		//
		PLU_DEVICE pDevice =(PLU_DEVICE)m_treeDevice.GetItemData(hItem);
		if(pDevice)
		{
			ShowDeviceInfo(pDevice);
		}
	}
	*pResult = 0;
}

void CLionSDKDemoDlg::ClearTreeState()
{
	HTREEITEM hRoot = m_treeDevice.GetRootItem();
	m_treeDevice.SetCheck(hRoot, FALSE);
	if(m_treeDevice.ItemHasChildren(hRoot))
	{
		ClearTreeNodeState(hRoot);
	}
	//
	hRoot = m_treeDevice.GetNextItem(hRoot, TVGN_NEXT);
	while(hRoot != NULL)
	{
		m_treeDevice.SetCheck(hRoot, FALSE);
		if(m_treeDevice.ItemHasChildren(hRoot))
		{
			ClearTreeNodeState(hRoot);
		}
		hRoot = m_treeDevice.GetNextItem(hRoot, TVGN_NEXT);
	}

}
void CLionSDKDemoDlg::ClearTreeNodeState(HTREEITEM hItem)
{
	HTREEITEM hChildItem = m_treeDevice.GetChildItem(hItem);
	while(hChildItem != NULL)
	{
		m_treeDevice.SetCheck(hChildItem, FALSE);
		if(m_treeDevice.ItemHasChildren(hChildItem))
		{
			ClearTreeNodeState(hChildItem);
		}
		//
		hChildItem = m_treeDevice.GetNextItem(hChildItem, TVGN_NEXT);
	}
}

void CLionSDKDemoDlg::OnBnClickedButtonOpen()
{
	//// TODO: Add your control notification handler code here
	//HTREEITEM hTree = m_treeDevice.GetSelectedItem();
	//if(hTree == 0)
	//{
	//	return;
	//}
	//m_btnOpen.EnableWindow(FALSE);
	//m_treeDevice.EnableWindow(FALSE);
	//m_btnEnum.EnableWindow(FALSE);
	//PLU_DEVICE pDevice =(PLU_DEVICE)m_treeDevice.GetItemData(hTree);
	//if(pDevice)
	//{
	//	if(LU_SUCCESS != OpenDevice(pDevice))
	//	{
	//		m_btnOpen.EnableWindow(TRUE);
	//		m_treeDevice.EnableWindow(TRUE);
	//		m_btnEnum.EnableWindow(TRUE);
	//		m_btnClose.EnableWindow(FALSE);
	//	}
	//	else
	//	{
	//		m_btnClose.EnableWindow(TRUE);
	//	}
	//}
	//else
	//{
	//	m_btnOpen.EnableWindow(TRUE);
	//	m_treeDevice.EnableWindow(TRUE);
	//	m_btnEnum.EnableWindow(TRUE);
	//	m_btnClose.EnableWindow(FALSE);
	//}
}

void CLionSDKDemoDlg::OnBnClickedButtonClose()
{
	//// TODO: Add your control notification handler code here
	//m_btnClose.EnableWindow(FALSE);
	//HTREEITEM hTree = m_treeDevice.GetSelectedItem();
	//PLU_DEVICE pDevice =(PLU_DEVICE)m_treeDevice.GetItemData(hTree);
	//if(pDevice)
	//{
	//	if(LU_FAIL == CloseDevice(pDevice))
	//	{
	//		m_btnClose.EnableWindow(FALSE);
	//	}
	//	else
	//	{
	//		m_btnOpen.EnableWindow(TRUE);
	//		m_treeDevice.EnableWindow(TRUE);
	//		m_btnEnum.EnableWindow(TRUE);
	//	}
	//}
}


void CLionSDKDemoDlg::OnBnClickedButtonUi()
{
	// TODO: Add your control notification handler code here
}


void CLionSDKDemoDlg::OnBnClickedButtonEnum()
{
	// TODO: Add your control notification handler code here
	m_treeDevice.DeleteAllItems();
	m_vcUVCDevice.clear();
	m_vcTwainDevice.clear();
	int nDev = GetDeviceCount();    //遍历UVC
	for(int d = 0; d < nDev; d++)   //遍历设备
	{
		PLU_DEVICE pDev = GetDevice(d);
		m_vcUVCDevice.push_back(*pDev);
	}
	
	
	//nDev = GetDeviceCount(0);		//遍历TW
	//for(int d = 0; d < nDev; d++)
	//{
	//	PLU_DEVICE pDev = GetDevice(d, 0);
	//	m_vcTwainDevice.push_back(*pDev);
	//}
	
	///
	///
	HTREEITEM hUVC = m_treeDevice.InsertItem(_T("UVC设备"));
	for(int i = 0; i < m_vcUVCDevice.size(); i++){
		WCHAR wcText[512] = { 0 };
		mbstowcs(wcText, m_vcUVCDevice[i].uvcIdentity.Name, sizeof(wcText));
		HTREEITEM hTree = m_treeDevice.InsertItem(wcText, hUVC);
		m_treeDevice.SetItemData(hTree, (DWORD_PTR)&m_vcUVCDevice[i]);
	}
	
	
	//HTREEITEM hTwain = m_treeDevice.InsertItem(_T("Twain设备"));
	//for(int i = 0; i < m_vcTwainDevice.size(); i++){
	//	WCHAR wcText[512] = { 0 };
	//	mbstowcs(wcText, m_vcTwainDevice[i].twIdentity.ProductName, sizeof(wcText));
	//	HTREEITEM hTree = m_treeDevice.InsertItem(wcText, hTwain);
	//	m_treeDevice.SetItemData(hTree, (DWORD_PTR)&m_vcTwainDevice[i]);
	//}
	
}



void CLionSDKDemoDlg::OnBnClickedButtonQuit()
{
	// TODO: Add your control notification handler code here

	m_treeDevice.DeleteAllItems();
	m_vcUVCDevice.erase(m_vcUVCDevice.begin(), m_vcUVCDevice.end());
	m_vcUVCDevice.erase(m_vcTwainDevice.begin(), m_vcTwainDevice.end());

	//_CrtDumpMemoryLeaks();
	//关闭一切，然后再退出
	CDialog::OnCancel();
}








//显示图像
LU_RESULT CLionSDKDemoDlg::ShowImageData(LU_DEVICE device, LU_PCHAR pImgData, LU_UINT32 nDataBuf, LU_PCHAR pFile, LU_UINT32 nFileBuf)
{
	///获取到图像文件。
	if(nFileBuf > 0 && strlen(pFile) == nFileBuf)
	{
		//转换成宽字符。
		WCHAR wcFile[256] = { 0 };
		MultiByteToWideChar(936, 0, pFile, nFileBuf, wcFile, 256);
		//将图像文件显示出来
		CImage img;
		img.Load(wcFile);
		CRect rect;
		m_stImage.GetClientRect(rect);
		img.Draw(m_stImage.GetDC()->m_hDC, rect); 

		img.Destroy();

	}
	m_btnAsynchronous.EnableWindow(TRUE);
	m_btnSynchronous.EnableWindow(TRUE);
	return 0;
}


//同步获取图像线程
DWORD CLionSDKDemoDlg::GetImageThreadProc(LPVOID lpParam)
{
	try
	{
		CLionSDKDemoDlg* pDlg = (CLionSDKDemoDlg*)lpParam;
		if(pDlg)
			pDlg->GetSynchronousImage();
	}
	catch(...)
	{
	}
	return 0;
}
//获取图像
void CLionSDKDemoDlg::GetSynchronousImage()
{
	//将选择的设备信息，获取相应的图像。现在每次设备访问，都是单次流程，获取完就自动关闭设备。
	HTREEITEM hTree = m_treeDevice.GetSelectedItem();
	PLU_DEVICE pDevice =(PLU_DEVICE)m_treeDevice.GetItemData(hTree);
	if(pDevice)
	{
		//控件失能
		m_btnSynchronous.EnableWindow(FALSE);
		m_btnAsynchronous.EnableWindow(FALSE);		
		WCHAR cLog[256] = { 0 };
		


		wsprintf(cLog, _T("设备状态: 获取图像"));
		m_stStateInfo.SetWindowText(cLog);
		//获取图像，返回图像文件。
		char cFile[256] = { 0 };
		unsigned int nSize = 0;
		unsigned int nFile = 256;
		//同步获取
		LU_RESULT lr = GetImage(pDevice, 1, NULL, &nSize, cFile, &nFile);
		////获取到图像文件。
		if(nFile > 0 && strlen(cFile) == nFile)
		{
			//转换成宽字符。
			WCHAR wcFile[256] = { 0 };
			MultiByteToWideChar(936, 0, cFile, nFile, wcFile, 256);
			//将图像文件显示出来
			CImage img;
			img.Load(wcFile);
			CRect rect;
			m_stImage.GetClientRect(rect);
			img.Draw(m_stImage.GetDC()->m_hDC, rect); 
			img.Destroy();

			wsprintf(cLog, _T("设备状态: 获取图像完成"));
		}
		else	
		{
			wsprintf(cLog, _T("设备状态: 获取图像  失败"));
		}
		m_stStateInfo.SetWindowText(cLog);
		
		
		//控件使能
		m_btnSynchronous.EnableWindow(TRUE);
		m_btnAsynchronous.EnableWindow(TRUE);
	}
}









//修改设备序列号
void CLionSDKDemoDlg::OnBnClickedButtonModifySerial()
{
	// TODO: Add your control notification handler code here
	//
	CString csSerial;
	m_etSerial.GetWindowText(csSerial);
	//获取当前的设备
	HTREEITEM hTree = m_treeDevice.GetSelectedItem();
	if(hTree == NULL)
		return;
	PLU_DEVICE pDevice =(PLU_DEVICE)m_treeDevice.GetItemData(hTree);
	if(pDevice)
	{
		char cSerial[64] = { 0 };
		wcstombs(cSerial, csSerial.GetBuffer(), 64);
		//修改
		int nRet = SetDeviceSerial(pDevice, cSerial);
		if(LU_SUCCESS == nRet)
		{
			//重新设置数据
			m_treeDevice.SetItemData(hTree, (DWORD_PTR)pDevice);
			WCHAR wcText[512] = { 0 };
			mbstowcs(wcText, pDevice->uvcIdentity.Name, sizeof(wcText));
			m_treeDevice.SetItemText(hTree, wcText);
			MessageBox(_T("设备序列号修改成功!"));
		}
		else
		{
			MessageBox(_T("设备序列号修改失败!"));
		}
	}
	else
	{
		//未选择设备
	}
}



//模式设置
void CLionSDKDemoDlg::OnBnClickedButtonModifyModel()
{
	// TODO: Add your control notification handler code here
	//
	//出图模式
	int nImgModel = m_cbModelIMg.GetCurSel();
	//Binning模式标志位
	int nBinningModel = m_cbBinning.GetCurSel();
	//图像处理标志位
	int nFilter = m_cbModelFilter.GetCurSel();
	//X-Ray类型
	int nXRay = m_cbModelX_Ray.GetCurSel();
	//检测图像时间
	CString csCheckTime;
	m_etCheckTime.GetWindowText(csCheckTime);
	int nCheckTime = _wtoi(csCheckTime.GetBuffer());
	//获取图像时间
	CString csGetTime;
	m_etGetTime.GetWindowText(csGetTime);
	int nGetTime = _wtoi(csGetTime.GetBuffer());
	//获取当前的设备
	HTREEITEM hTree = m_treeDevice.GetSelectedItem();
	if(hTree == NULL)
	{
		MessageBox(_T("未选中设备"));
		return;
	}
	PLU_DEVICE pDevice =(PLU_DEVICE)m_treeDevice.GetItemData(hTree);
	if(pDevice)
	{
		//一次只能设置一个参数
		LU_PARAM  param;            
		memset(&param, 0, sizeof(param));
		param.param = LUDEVPARAM_MODE;
		param.data = &nImgModel;
		SetDeviceParam(pDevice, &param);
		//
		param.param = LUDEVPARAM_BINNING;
		param.data = &nBinningModel;
		SetDeviceParam(pDevice, &param);
		//
		param.param = LUDEVPARAM_FILTER;
		param.data = &nFilter;
		SetDeviceParam(pDevice, &param);
		//
		param.param = LUDEVPARAM_XRAY;
		param.data = &nXRay;
		SetDeviceParam(pDevice, &param);
		//
		//
		param.param = LUDEVPARAM_TRIGGERTIME;
		param.data = &nCheckTime;
		SetDeviceParam(pDevice, &param);
		//
		param.param = LUDEVPARAM_READIMAGETIME;
		param.data = &nGetTime;
		SetDeviceParam(pDevice, &param);
		

	}
}






/****************************************************************************************************
****************************************************************************************************/
void CLionSDKDemoDlg::OnEnChangeEditBrightness()
{
	// TODO:  If this is a RICHEDIT control, the control will not
	// send this notification unless you override the CDialog::OnInitDialog()
	// function and call CRichEditCtrl().SetEventMask()
	// with the ENM_CHANGE flag ORed into the mask.

	// TODO:  Add your control notification handler code here
	//
	CString csText;
	m_etBrightness.GetWindowText(csText);
	//

	int nValue = _wtoi(csText.GetBuffer());
	if(nValue < 0)
		nValue = 0;
	if(nValue > 100)
		nValue = 100;
	//
	m_scBrightness.SetPos(nValue);
	//
	SetImageDataParam(LUDEVPARAM_BRIGHTNESS, nValue);
}

void CLionSDKDemoDlg::OnEnChangeEditContrast()
{
	// TODO:  If this is a RICHEDIT control, the control will not
	// send this notification unless you override the CDialog::OnInitDialog()
	// function and call CRichEditCtrl().SetEventMask()
	// with the ENM_CHANGE flag ORed into the mask.

	// TODO:  Add your control notification handler code here
	CString csText;
	m_etContrast.GetWindowText(csText);
	//
	int nValue = _wtoi(csText.GetBuffer());
	if(nValue < 0)
		nValue = 0;
	if(nValue > 100)
		nValue = 100;
	//
	m_scContrast.SetPos(nValue);
	//
	SetImageDataParam(LUDEVPARAM_CONTRAST, nValue);
}

void CLionSDKDemoDlg::OnEnChangeEditSaturation()
{
	// TODO:  If this is a RICHEDIT control, the control will not
	// send this notification unless you override the CDialog::OnInitDialog()
	// function and call CRichEditCtrl().SetEventMask()
	// with the ENM_CHANGE flag ORed into the mask.

	// TODO:  Add your control notification handler code here
	CString csText;
	m_etSaturation.GetWindowText(csText);
	//
	int nValue = _wtoi(csText.GetBuffer());
	if(nValue < 0)
		nValue = 0;
	if(nValue > 100)
		nValue = 100;
	//
	m_scSaturation.SetPos(nValue);
	//
	//
	SetImageDataParam(LUDEVPARAM_SATURATION, nValue);
}
void CLionSDKDemoDlg::OnEnChangeEditSharpen()
{
	// TODO:  If this is a RICHEDIT control, the control will not
	// send this notification unless you override the CDialog::OnInitDialog()
	// function and call CRichEditCtrl().SetEventMask()
	// with the ENM_CHANGE flag ORed into the mask.

	// TODO:  Add your control notification handler code here
	CString csText;
	m_etSHarpen.GetWindowText(csText);
	//
	int nValue = _wtoi(csText.GetBuffer());
	if(nValue < 0)
		nValue = 0;
	if(nValue > 100)
		nValue = 100;
	//
	m_scSharpen.SetPos(nValue);
	//
	//
	SetImageDataParam(LUDEVPARAM_SHARPEN, nValue);
}
void CLionSDKDemoDlg::OnEnChangeEditEmboss()
{
	// TODO:  If this is a RICHEDIT control, the control will not
	// send this notification unless you override the CDialog::OnInitDialog()
	// function and call CRichEditCtrl().SetEventMask()
	// with the ENM_CHANGE flag ORed into the mask.

	// TODO:  Add your control notification handler code here

	CString csText;
	m_etEmboss.GetWindowText(csText);
	//
	int nValue = _wtoi(csText.GetBuffer());
	if(nValue < 0)
		nValue = 0;
	if(nValue > 100)
		nValue = 100;
	//
	m_scEmboss.SetPos(nValue);
	//
	//
	SetImageDataParam(LUDEVPARAM_EMBOSS, nValue);
}
void CLionSDKDemoDlg::OnEnChangeEditBlur()
{
	// TODO:  If this is a RICHEDIT control, the control will not
	// send this notification unless you override the CDialog::OnInitDialog()
	// function and call CRichEditCtrl().SetEventMask()
	// with the ENM_CHANGE flag ORed into the mask.

	// TODO:  Add your control notification handler code here
	CString csText;
	m_etBlur.GetWindowText(csText);
	//
	int nValue = _wtoi(csText.GetBuffer());
	if(nValue < 0)
		nValue = 0;
	if(nValue > 100)
		nValue = 100;
	//
	m_scBlur.SetPos(nValue);
	//
	//
	SetImageDataParam(LUDEVPARAM_BLUR, nValue);
}


void CLionSDKDemoDlg::OnEnChangeEditGamma()
{
	// TODO:  If this is a RICHEDIT control, the control will not
	// send this notification unless you override the CDialog::OnInitDialog()
	// function and call CRichEditCtrl().SetEventMask()
	// with the ENM_CHANGE flag ORed into the mask.

	// TODO:  Add your control notification handler code here

	CString csText;
	m_etGamma.GetWindowText(csText);
	//
	int nValue = _wtoi(csText.GetBuffer());
	if(nValue < 0)
		nValue = 0;
	if(nValue > 100)
		nValue = 100;
	//
	m_scGamma.SetPos(nValue);
	//
	//
	SetImageDataParam(LUDEVPARAM_GAMMA, nValue);
}




void CLionSDKDemoDlg::OnHScroll(UINT nSBCode, UINT nPos, CScrollBar* pScrollBar)  
{  
    // TODO: 在此添加消息处理程序代码和/或调用默认值  
    if(GetDlgItem(IDC_SLIDER_BRIGHTNESS) == pScrollBar)  
    {  
        //MessageBox(L"Slider1");  
        switch(nSBCode)  
        {  
        case SB_PAGELEFT://影响鼠标点击一次滑块或用户按下PageUp和PageDown键，滑块移动的距离，由SetPageSize决定  
        case SB_PAGERIGHT:  
        case SB_LEFT://用户按下Home键触发  
        case SB_RIGHT://用户按下End键触发  
        case SB_LINELEFT://用户按下↑↓←→键时触发  
        case SB_LINERIGHT://用户按下↑↓←→键时触发  
			{
				int nValue = m_scBrightness.GetPos();//def :128  
				CString csText;
				csText.Format(_T("%d"), nValue);
				m_etBrightness.SetWindowText(csText);
			}
            break;  
        case SB_THUMBPOSITION://鼠标拖拽滑块并释放时触发，此时nPos有效  
        case SB_THUMBTRACK://鼠标拖拽滑块时触发，此时nPos有效  
			{
				int nValue = m_scBrightness.GetPos();//def :128  
				CString csText;
				csText.Format(_T("%d"), nValue);
				m_etBrightness.SetWindowText(csText);
			}
            break;  
        default:  
            break;  
        }  
    }
	else if(GetDlgItem(IDC_SLIDER_CONTRAST) == pScrollBar)  
    {  
        //MessageBox(L"Slider1");  
        switch(nSBCode)  
        {  
        case SB_PAGELEFT://影响鼠标点击一次滑块或用户按下PageUp和PageDown键，滑块移动的距离，由SetPageSize决定  
        case SB_PAGERIGHT:  
        case SB_LEFT://用户按下Home键触发  
        case SB_RIGHT://用户按下End键触发  
        case SB_LINELEFT://用户按下↑↓←→键时触发  
        case SB_LINERIGHT://用户按下↑↓←→键时触发  
        case SB_THUMBPOSITION://鼠标拖拽滑块并释放时触发，此时nPos有效  
        case SB_THUMBTRACK://鼠标拖拽滑块时触发，此时nPos有效  
			{
				int nValue = m_scContrast.GetPos();//def :128  
				CString csText;
				csText.Format(_T("%d"), nValue);
				m_etContrast.SetWindowText(csText);
			}
            break;  
        default:  
            break;  
        }  
    }
	else if(GetDlgItem(IDC_SLIDER_SATURATION) == pScrollBar)  
    {  
        //MessageBox(L"Slider1");  
        switch(nSBCode)  
        {  
        case SB_PAGELEFT://影响鼠标点击一次滑块或用户按下PageUp和PageDown键，滑块移动的距离，由SetPageSize决定  
        case SB_PAGERIGHT:  
        case SB_LEFT://用户按下Home键触发  
        case SB_RIGHT://用户按下End键触发  
        case SB_LINELEFT://用户按下↑↓←→键时触发  
        case SB_LINERIGHT://用户按下↑↓←→键时触发  
        case SB_THUMBPOSITION://鼠标拖拽滑块并释放时触发，此时nPos有效  
        case SB_THUMBTRACK://鼠标拖拽滑块时触发，此时nPos有效  
			{
				int nValue = m_scSaturation.GetPos();//def :128  
				CString csText;
				csText.Format(_T("%d"), nValue);
				m_etSaturation.SetWindowText(csText);
			}
            break;  
        default:  
            break;  
        }  
    }
	else if(GetDlgItem(IDC_SLIDER_SHARPEN) == pScrollBar)  
    {  
        //MessageBox(L"Slider1");  
        switch(nSBCode)  
        {  
        case SB_PAGELEFT://影响鼠标点击一次滑块或用户按下PageUp和PageDown键，滑块移动的距离，由SetPageSize决定  
        case SB_PAGERIGHT:  
        case SB_LEFT://用户按下Home键触发  
        case SB_RIGHT://用户按下End键触发  
        case SB_LINELEFT://用户按下↑↓←→键时触发  
        case SB_LINERIGHT://用户按下↑↓←→键时触发  
        case SB_THUMBPOSITION://鼠标拖拽滑块并释放时触发，此时nPos有效  
        case SB_THUMBTRACK://鼠标拖拽滑块时触发，此时nPos有效  
			{
				int nValue = m_scSharpen.GetPos();//def :128  
				CString csText;
				csText.Format(_T("%d"), nValue);
				m_etSHarpen.SetWindowText(csText);
			}
            break;  
        default:  
            break;  
        }  
    }
	else if(GetDlgItem(IDC_SLIDER_EMBOSS) == pScrollBar)  
    {  
        //MessageBox(L"Slider1");  
        switch(nSBCode)  
        {  
        case SB_PAGELEFT://影响鼠标点击一次滑块或用户按下PageUp和PageDown键，滑块移动的距离，由SetPageSize决定  
        case SB_PAGERIGHT:  
        case SB_LEFT://用户按下Home键触发  
        case SB_RIGHT://用户按下End键触发  
        case SB_LINELEFT://用户按下↑↓←→键时触发  
        case SB_LINERIGHT://用户按下↑↓←→键时触发  
        case SB_THUMBPOSITION://鼠标拖拽滑块并释放时触发，此时nPos有效  
        case SB_THUMBTRACK://鼠标拖拽滑块时触发，此时nPos有效  
			{
				int nValue = m_scEmboss.GetPos();//def :128  
				CString csText;
				csText.Format(_T("%d"), nValue);
				m_etEmboss.SetWindowText(csText);
			}
            break;  
        default:  
            break;  
        }  
    }
	else if(GetDlgItem(IDC_SLIDER_BLUR) == pScrollBar)  
    {  
        //MessageBox(L"Slider1");  
        switch(nSBCode)  
        {  
        case SB_PAGELEFT://影响鼠标点击一次滑块或用户按下PageUp和PageDown键，滑块移动的距离，由SetPageSize决定  
        case SB_PAGERIGHT:  
        case SB_LEFT://用户按下Home键触发  
        case SB_RIGHT://用户按下End键触发  
        case SB_LINELEFT://用户按下↑↓←→键时触发  
        case SB_LINERIGHT://用户按下↑↓←→键时触发  
        case SB_THUMBPOSITION://鼠标拖拽滑块并释放时触发，此时nPos有效  
        case SB_THUMBTRACK://鼠标拖拽滑块时触发，此时nPos有效  
			{
				int nValue = m_scBlur.GetPos();//def :128  
				CString csText;
				csText.Format(_T("%d"), nValue);
				m_etBlur.SetWindowText(csText);
			}
            break;  
        default:  
            break;  
        }  
    }
	else if(GetDlgItem(IDC_SLIDER_GAMMA) == pScrollBar)  
    {  
        //MessageBox(L"Slider1");  
        switch(nSBCode)  
        {  
        case SB_PAGELEFT://影响鼠标点击一次滑块或用户按下PageUp和PageDown键，滑块移动的距离，由SetPageSize决定  
        case SB_PAGERIGHT:  
        case SB_LEFT://用户按下Home键触发  
        case SB_RIGHT://用户按下End键触发  
        case SB_LINELEFT://用户按下↑↓←→键时触发  
        case SB_LINERIGHT://用户按下↑↓←→键时触发  
        case SB_THUMBPOSITION://鼠标拖拽滑块并释放时触发，此时nPos有效  
        case SB_THUMBTRACK://鼠标拖拽滑块时触发，此时nPos有效  
			{
				int nValue = m_scGamma.GetPos();//def :128  
				CString csText;
				csText.Format(_T("%d"), nValue);
				m_etGamma.SetWindowText(csText);
			}
            break;  
        default:  
            break;  
        }  
    }


    CDialog::OnHScroll(nSBCode, nPos, pScrollBar);  
}  


//
//显示设备信息
void CLionSDKDemoDlg::ShowDeviceInfo(PLU_DEVICE pDevice)
{
	if(pDevice->uvcIdentity.Id > 0)
	{
		WCHAR wcName[128] = { 0 }; 
		WCHAR wcSerial[128] = { 0 };
		mbstowcs(wcName, pDevice->uvcIdentity.Name, 128);
		mbstowcs(wcSerial, pDevice->uvcIdentity.DevSerial, 128);
		CString csText;
		csText.Format(_T("设备ID: %d \r\n设备名称: %s \r\n设备序列号: %s\r\n设备版本:%d.%d"), 
			pDevice->uvcIdentity.Id, wcName, wcSerial, 
			pDevice->uvcIdentity.MajorNum, pDevice->uvcIdentity.MinorNum);
		m_stDeviceInfo.SetWindowText(csText);
	}
	else if(pDevice->twIdentity.Id > 0)
	{
		WCHAR wcManufacturer[128] = { 0 };
		WCHAR wcProductFamily[128] = { 0 };
		WCHAR wcProductName[128] = { 0 };
		mbstowcs(wcManufacturer, pDevice->twIdentity.Manufacturer, 128);
		mbstowcs(wcProductFamily, pDevice->twIdentity.ProductFamily, 128);
		mbstowcs(wcProductName, pDevice->twIdentity.ProductName, 128);
		CString csText;
		csText.Format(_T("设备ID: %d \r\nManufacturer: %s \r\nProductFamily: %s\r\nProductName: %s\r\n协议版本:%d.%d"), 
			pDevice->twIdentity.Id, wcManufacturer, wcProductFamily, 
			wcProductName, pDevice->twIdentity.ProtocolMajor, pDevice->twIdentity.ProtocolMinor);
		m_stDeviceInfo.SetWindowText(csText);
	}
}
//设备图像参数
void CLionSDKDemoDlg::SetImageDataParam(int nParam, int nValue)
{
	HTREEITEM hTree = m_treeDevice.GetSelectedItem();
	if(hTree == NULL)
		return;
	PLU_DEVICE pDevice =(PLU_DEVICE)m_treeDevice.GetItemData(hTree);
	if(pDevice)
	{
		LU_PARAM luParam = { 0 };
		luParam.param = nParam;
		luParam.type = LUTY_INT32;
		luParam.size = sizeof(nValue);
		luParam.data = &nValue;
		SetDeviceParam(pDevice, &luParam);
	}
}


//获取设备状态
void CLionSDKDemoDlg::OnBnClickedButtonGetstate()
{
	// TODO: Add your control notification handler code here
	HTREEITEM hTree = m_treeDevice.GetSelectedItem();
	if(hTree == NULL)
		return;
	PLU_DEVICE pDevice =(PLU_DEVICE)m_treeDevice.GetItemData(hTree);
	if(pDevice)
	{
		//当前设备状态值
		CString csStateInfo;
		LUDEV_STATE dev_state;
		switch(GetDeviceState(pDevice, &dev_state))
		{
		case LU_NOTSUPPORT:			//不支持
			csStateInfo = _T("设备状态: 不支持!");
			break;
		case LU_PARAMINVALID:		//无效参数
			csStateInfo = _T("设备状态: 无效参数!");
			break;
		case LU_SUCCESS:
			{
				switch(dev_state)
				{
				case LUDEVSTATE_UNOPNE:				//设备未打开
					csStateInfo = _T("设备状态: 设备未打开!");
					break;
				case LUDEVSTATE_OPEN:					//设备打开
					csStateInfo = _T("设备状态: 设备打开!");
					break;
				case LUDEVSTATE_WAITTRIGGER:					//等待触发
					csStateInfo = _T("设备状态: 等待触发!");
					break;
				case LUDEVSTATE_TRIGGERIMAGE:					//触发获取图像数据
					csStateInfo = _T("设备状态: 触发获取图像数据!");
					break;
				case LUDEVSTATE_IMAGESAVE:					//图像保存
					csStateInfo = _T("设备状态: 图像保存!");
					break;
				case LUDEVSTATE_OVERTIME:					//超时
					csStateInfo = _T("设备状态: 获取图像超时");
					break;
				}
			}
			break;
		default:
					csStateInfo = _T("设备状态: 无法获取!");
			break;
		}
		//
		m_stStateInfo.SetWindowText(csStateInfo);
		//

	}
}

//中断获取图像
void CLionSDKDemoDlg::OnBnClickedButtonAbandonimg()
{
	// TODO: Add your control notification handler code here
	HTREEITEM hTree = m_treeDevice.GetSelectedItem();
	if(hTree == NULL)
		return;
	PLU_DEVICE pDevice =(PLU_DEVICE)m_treeDevice.GetItemData(hTree);
	if(pDevice)
	{
		AbandonGetImage(pDevice);
	}
}

//同步方式获取，建议采用新线程获取图像，不阻塞主线程
void CLionSDKDemoDlg::OnBnClickedButtonSynchronous()
{
	// TODO: Add your control notification handler code here
	//同步获取图像，会阻塞界面线程，建议在工作线程中获取。
	CreateThread(0, 0, GetImageThreadProc, this, 0, 0);
	
}

//异步方式获取中断
void CLionSDKDemoDlg::OnBnClickedButtonAsynchronous()
{
	// TODO: Add your control notification handler code here
	//异步方式一
	////此时在界面线程中，单个异步获取不会阻塞界面线程，如果多次异步获取图像，一定是会阻塞界面线程，所以多次测试，建议开启一个新的工作线程处理。
	//if(m_hAsyThread != NULL)
	//{
	//	//当前已有线程在运行，需要先结束，再开启新的。
	//	return;
	//}
	//m_hAsyThread = CreateThread(0, 0, AsynchronousGetImageThreadProc, this, 0, 0);
	//if(m_hAsyThread == NULL)
	//{
	//	MessageBox(_T("批处理异步线程创建失败!!!"));
	//	return;
	//}

	//异步方式二
	//异步方式获取图片
	HTREEITEM hTree = m_treeDevice.GetSelectedItem();
	PLU_DEVICE pDevice =(PLU_DEVICE)m_treeDevice.GetItemData(hTree);
	if(pDevice)
	{
		//
		m_btnAsynchronous.EnableWindow(FALSE);
		m_btnSynchronous.EnableWindow(FALSE);
		//获取图像，返回图像文件。
		GetImage(pDevice, 0, ImageCallback);
	}
	
}



/***********************************************************************************************
*批量异步获取图像
***********************************************************************************************/
//批量异步操作获取图像线程
DWORD CLionSDKDemoDlg::AsynchronousGetImageThreadProc(LPVOID lpParam)
{
	CLionSDKDemoDlg* pDlg = (CLionSDKDemoDlg*)lpParam;
	ASSERT(pDlg);
	try
	{
		if(pDlg)
			pDlg->AsynchronousGetImage();
	}
	catch(...)
	{
	}
	//关闭线程句柄，清空。
	CloseHandle(pDlg->m_hAsyThread);
	pDlg->m_hAsyThread = NULL;
	return 0;
}
//获取图像
void CLionSDKDemoDlg::AsynchronousGetImage()
{
	//异步方式获取图片
	HTREEITEM hTree = m_treeDevice.GetSelectedItem();
	PLU_DEVICE pDevice =(PLU_DEVICE)m_treeDevice.GetItemData(hTree);
	if(pDevice)
	{
		//创建同步句柄
		m_hAsyEvent = CreateEvent(NULL, FALSE, FALSE, NULL);
		if(m_hAsyEvent == NULL)
		{
			//创建失败，退出
			return;
		}
		//
		m_btnAsynchronous.EnableWindow(FALSE);
		m_btnSynchronous.EnableWindow(FALSE);
		m_btnAbandon.EnableWindow(FALSE);					//将中断按钮也失能,在批处理中，此按钮没有实际作用。
		//
		int nCount = 2000;
		//
		CImage img;
		WCHAR wcFile[256] = { 0 };
		CString csNumInfo;
		CString csNumCancel;
		CString csStateInfo;
		CRect rect;
		HRESULT hresult1;
		//
		char cLog[256] = { 0 };
		//
		
		//复位
		ResetEvent(m_hAsyEvent);
		m_nFile = 0;
		memset(m_cFile, 0, sizeof(m_cFile));
		//获取图像，返回图像文件。
		GetImage(pDevice, 0, ImageCallback);
		//
		int nTm = 20000;
		//
		DWORD dwObj = WaitForSingleObject(m_hAsyEvent, nTm);
		if(dwObj == WAIT_OBJECT_0)
		{
			//操作完成,获取到图像文件。
			if (m_nFile > 0 && strlen(m_cFile) == m_nFile)
			{
				//转换成宽字符。
				MultiByteToWideChar(936, 0, m_cFile, m_nFile, wcFile, 256);
				//加载图像文件，显示出来
				hresult1 = img.Load(wcFile);
				m_stImage.GetClientRect(rect);
				if (E_FAIL == hresult1)
				{
					MessageBox(_T(" m_image Load  failed!"));
					return;
				}
				img.Draw(m_stImage.GetDC()->m_hDC, rect);
				csStateInfo = _T("设备状态: 读图完毕! 桢： ");
				//TRACE("设备状态: 读图完毕\r\n");
				img.Destroy();

			}
			else
			{
				TRACE("设备状态: 读图故障\r\n");
				csStateInfo = _T("设备状态: 读图故障! 桢： ");
			}
		}
		else
		{
			//超时，直接使用接口处理。
			AbandonGetImage(pDevice);
			//再次等待检测是否完成
			dwObj = WaitForSingleObject(m_hAsyEvent, 10);
			if(dwObj == WAIT_OBJECT_0)
			{
				//已完成，需要判断，是图像读取完成，还是取消完成,
				if (m_nFile > 0 && strlen(m_cFile) == m_nFile)
				{
					//正常读取成功。
					TRACE("设备状态: 取消操作，读图完毕!\r\n");
					csStateInfo = _T("设备状态: 取消操作，读图完毕! 桢： ");
				}
				else
				{
					//取消成功
					TRACE("设备状态: 取消操作，取消成功!\r\n");
					csStateInfo = _T("设备状态: 取消操作，取消成功! 桢： ");
				}
			}
			else
			{
				//超时，取消操作还未完成。
				TRACE("设备状态: 取消操作，取消超时!\r\n");
				csStateInfo = _T("设备状态: 取消操作，取消超时! 桢： ");
			}
		}
		m_stStateInfo.SetWindowText(csStateInfo);

		//释放
		CloseHandle(m_hAsyEvent);
		m_hAsyEvent = NULL;
		//使能
		m_btnAsynchronous.EnableWindow(TRUE);
		m_btnSynchronous.EnableWindow(TRUE);
		m_btnAbandon.EnableWindow(TRUE);
	}
}


LU_RESULT CLionSDKDemoDlg::ImageCallback(LU_DEVICE device, LU_PCHAR pImgData, LU_UINT32 nDataBuf, LU_PCHAR pFile, LU_UINT32 nFileBuf)
{
	
	////方式一，通过线程异步调用。
	//TRACE("ImageCallBack\r\n");
	////返回图像文件名称长度
	//pSDKDemoDlg->m_nFile = nFileBuf;
	////返回图像文件名称
	//if(pSDKDemoDlg->m_nFile > 0)
	//	memcpy(pSDKDemoDlg->m_cFile, pFile, nFileBuf);
	////图像获取完成
	//if(pSDKDemoDlg->m_hAsyEvent != NULL)
	//	SetEvent(pSDKDemoDlg->m_hAsyEvent);

	//方式，通过界面线程，异步调用
	pSDKDemoDlg->ShowImageData(device, pImgData, nDataBuf, pFile, nFileBuf);

	return 0;
}


