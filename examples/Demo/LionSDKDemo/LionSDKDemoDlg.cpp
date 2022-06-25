
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
	//�첽�����߳̾��
	m_hAsyThread = NULL;
	m_hAsyEvent = NULL;
	//ͬ�������߳̾��
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
	m_cbModelIMg.InsertString(0, _T("AC ��ͼ"));
	m_cbModelIMg.InsertString(1, _T("VTC ��ͼ"));
	m_cbModelIMg.SetCurSel(0);

	m_cbBinning.Clear();
	m_cbBinning.InsertString(0, _T("No Binning"));
	m_cbBinning.InsertString(1, _T("Binning"));
	m_cbBinning.SetCurSel(0);
	
	m_cbModelFilter.Clear();
	m_cbModelFilter.InsertString(0, _T("ԭʼͼ��"));
	m_cbModelFilter.InsertString(1, _T("FPGA ���л������"));
	m_cbModelFilter.SetCurSel(0);
	
	m_cbModelX_Ray.Clear();
	m_cbModelX_Ray.InsertString(0, _T("VTC-D"));
	m_cbModelX_Ray.InsertString(1, _T("VTC-A"));
	m_cbModelX_Ray.SetCurSel(0);

	//���ʱ��
	m_etCheckTime.SetWindowText(_T("5000"));
	//��ͼʱ��
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
		//���ѡ����
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
	int nDev = GetDeviceCount();    //����UVC
	for(int d = 0; d < nDev; d++)   //�����豸
	{
		PLU_DEVICE pDev = GetDevice(d);
		m_vcUVCDevice.push_back(*pDev);
	}
	
	
	//nDev = GetDeviceCount(0);		//����TW
	//for(int d = 0; d < nDev; d++)
	//{
	//	PLU_DEVICE pDev = GetDevice(d, 0);
	//	m_vcTwainDevice.push_back(*pDev);
	//}
	
	///
	///
	HTREEITEM hUVC = m_treeDevice.InsertItem(_T("UVC�豸"));
	for(int i = 0; i < m_vcUVCDevice.size(); i++){
		WCHAR wcText[512] = { 0 };
		mbstowcs(wcText, m_vcUVCDevice[i].uvcIdentity.Name, sizeof(wcText));
		HTREEITEM hTree = m_treeDevice.InsertItem(wcText, hUVC);
		m_treeDevice.SetItemData(hTree, (DWORD_PTR)&m_vcUVCDevice[i]);
	}
	
	
	//HTREEITEM hTwain = m_treeDevice.InsertItem(_T("Twain�豸"));
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
	//�ر�һ�У�Ȼ�����˳�
	CDialog::OnCancel();
}








//��ʾͼ��
LU_RESULT CLionSDKDemoDlg::ShowImageData(LU_DEVICE device, LU_PCHAR pImgData, LU_UINT32 nDataBuf, LU_PCHAR pFile, LU_UINT32 nFileBuf)
{
	///��ȡ��ͼ���ļ���
	if(nFileBuf > 0 && strlen(pFile) == nFileBuf)
	{
		//ת���ɿ��ַ���
		WCHAR wcFile[256] = { 0 };
		MultiByteToWideChar(936, 0, pFile, nFileBuf, wcFile, 256);
		//��ͼ���ļ���ʾ����
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


//ͬ����ȡͼ���߳�
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
//��ȡͼ��
void CLionSDKDemoDlg::GetSynchronousImage()
{
	//��ѡ����豸��Ϣ����ȡ��Ӧ��ͼ������ÿ���豸���ʣ����ǵ������̣���ȡ����Զ��ر��豸��
	HTREEITEM hTree = m_treeDevice.GetSelectedItem();
	PLU_DEVICE pDevice =(PLU_DEVICE)m_treeDevice.GetItemData(hTree);
	if(pDevice)
	{
		//�ؼ�ʧ��
		m_btnSynchronous.EnableWindow(FALSE);
		m_btnAsynchronous.EnableWindow(FALSE);		
		WCHAR cLog[256] = { 0 };
		


		wsprintf(cLog, _T("�豸״̬: ��ȡͼ��"));
		m_stStateInfo.SetWindowText(cLog);
		//��ȡͼ�񣬷���ͼ���ļ���
		char cFile[256] = { 0 };
		unsigned int nSize = 0;
		unsigned int nFile = 256;
		//ͬ����ȡ
		LU_RESULT lr = GetImage(pDevice, 1, NULL, &nSize, cFile, &nFile);
		////��ȡ��ͼ���ļ���
		if(nFile > 0 && strlen(cFile) == nFile)
		{
			//ת���ɿ��ַ���
			WCHAR wcFile[256] = { 0 };
			MultiByteToWideChar(936, 0, cFile, nFile, wcFile, 256);
			//��ͼ���ļ���ʾ����
			CImage img;
			img.Load(wcFile);
			CRect rect;
			m_stImage.GetClientRect(rect);
			img.Draw(m_stImage.GetDC()->m_hDC, rect); 
			img.Destroy();

			wsprintf(cLog, _T("�豸״̬: ��ȡͼ�����"));
		}
		else	
		{
			wsprintf(cLog, _T("�豸״̬: ��ȡͼ��  ʧ��"));
		}
		m_stStateInfo.SetWindowText(cLog);
		
		
		//�ؼ�ʹ��
		m_btnSynchronous.EnableWindow(TRUE);
		m_btnAsynchronous.EnableWindow(TRUE);
	}
}









//�޸��豸���к�
void CLionSDKDemoDlg::OnBnClickedButtonModifySerial()
{
	// TODO: Add your control notification handler code here
	//
	CString csSerial;
	m_etSerial.GetWindowText(csSerial);
	//��ȡ��ǰ���豸
	HTREEITEM hTree = m_treeDevice.GetSelectedItem();
	if(hTree == NULL)
		return;
	PLU_DEVICE pDevice =(PLU_DEVICE)m_treeDevice.GetItemData(hTree);
	if(pDevice)
	{
		char cSerial[64] = { 0 };
		wcstombs(cSerial, csSerial.GetBuffer(), 64);
		//�޸�
		int nRet = SetDeviceSerial(pDevice, cSerial);
		if(LU_SUCCESS == nRet)
		{
			//������������
			m_treeDevice.SetItemData(hTree, (DWORD_PTR)pDevice);
			WCHAR wcText[512] = { 0 };
			mbstowcs(wcText, pDevice->uvcIdentity.Name, sizeof(wcText));
			m_treeDevice.SetItemText(hTree, wcText);
			MessageBox(_T("�豸���к��޸ĳɹ�!"));
		}
		else
		{
			MessageBox(_T("�豸���к��޸�ʧ��!"));
		}
	}
	else
	{
		//δѡ���豸
	}
}



//ģʽ����
void CLionSDKDemoDlg::OnBnClickedButtonModifyModel()
{
	// TODO: Add your control notification handler code here
	//
	//��ͼģʽ
	int nImgModel = m_cbModelIMg.GetCurSel();
	//Binningģʽ��־λ
	int nBinningModel = m_cbBinning.GetCurSel();
	//ͼ�����־λ
	int nFilter = m_cbModelFilter.GetCurSel();
	//X-Ray����
	int nXRay = m_cbModelX_Ray.GetCurSel();
	//���ͼ��ʱ��
	CString csCheckTime;
	m_etCheckTime.GetWindowText(csCheckTime);
	int nCheckTime = _wtoi(csCheckTime.GetBuffer());
	//��ȡͼ��ʱ��
	CString csGetTime;
	m_etGetTime.GetWindowText(csGetTime);
	int nGetTime = _wtoi(csGetTime.GetBuffer());
	//��ȡ��ǰ���豸
	HTREEITEM hTree = m_treeDevice.GetSelectedItem();
	if(hTree == NULL)
	{
		MessageBox(_T("δѡ���豸"));
		return;
	}
	PLU_DEVICE pDevice =(PLU_DEVICE)m_treeDevice.GetItemData(hTree);
	if(pDevice)
	{
		//һ��ֻ������һ������
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
    // TODO: �ڴ������Ϣ�����������/�����Ĭ��ֵ  
    if(GetDlgItem(IDC_SLIDER_BRIGHTNESS) == pScrollBar)  
    {  
        //MessageBox(L"Slider1");  
        switch(nSBCode)  
        {  
        case SB_PAGELEFT://Ӱ�������һ�λ�����û�����PageUp��PageDown���������ƶ��ľ��룬��SetPageSize����  
        case SB_PAGERIGHT:  
        case SB_LEFT://�û�����Home������  
        case SB_RIGHT://�û�����End������  
        case SB_LINELEFT://�û����¡���������ʱ����  
        case SB_LINERIGHT://�û����¡���������ʱ����  
			{
				int nValue = m_scBrightness.GetPos();//def :128  
				CString csText;
				csText.Format(_T("%d"), nValue);
				m_etBrightness.SetWindowText(csText);
			}
            break;  
        case SB_THUMBPOSITION://�����ק���鲢�ͷ�ʱ��������ʱnPos��Ч  
        case SB_THUMBTRACK://�����ק����ʱ��������ʱnPos��Ч  
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
        case SB_PAGELEFT://Ӱ�������һ�λ�����û�����PageUp��PageDown���������ƶ��ľ��룬��SetPageSize����  
        case SB_PAGERIGHT:  
        case SB_LEFT://�û�����Home������  
        case SB_RIGHT://�û�����End������  
        case SB_LINELEFT://�û����¡���������ʱ����  
        case SB_LINERIGHT://�û����¡���������ʱ����  
        case SB_THUMBPOSITION://�����ק���鲢�ͷ�ʱ��������ʱnPos��Ч  
        case SB_THUMBTRACK://�����ק����ʱ��������ʱnPos��Ч  
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
        case SB_PAGELEFT://Ӱ�������һ�λ�����û�����PageUp��PageDown���������ƶ��ľ��룬��SetPageSize����  
        case SB_PAGERIGHT:  
        case SB_LEFT://�û�����Home������  
        case SB_RIGHT://�û�����End������  
        case SB_LINELEFT://�û����¡���������ʱ����  
        case SB_LINERIGHT://�û����¡���������ʱ����  
        case SB_THUMBPOSITION://�����ק���鲢�ͷ�ʱ��������ʱnPos��Ч  
        case SB_THUMBTRACK://�����ק����ʱ��������ʱnPos��Ч  
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
        case SB_PAGELEFT://Ӱ�������һ�λ�����û�����PageUp��PageDown���������ƶ��ľ��룬��SetPageSize����  
        case SB_PAGERIGHT:  
        case SB_LEFT://�û�����Home������  
        case SB_RIGHT://�û�����End������  
        case SB_LINELEFT://�û����¡���������ʱ����  
        case SB_LINERIGHT://�û����¡���������ʱ����  
        case SB_THUMBPOSITION://�����ק���鲢�ͷ�ʱ��������ʱnPos��Ч  
        case SB_THUMBTRACK://�����ק����ʱ��������ʱnPos��Ч  
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
        case SB_PAGELEFT://Ӱ�������һ�λ�����û�����PageUp��PageDown���������ƶ��ľ��룬��SetPageSize����  
        case SB_PAGERIGHT:  
        case SB_LEFT://�û�����Home������  
        case SB_RIGHT://�û�����End������  
        case SB_LINELEFT://�û����¡���������ʱ����  
        case SB_LINERIGHT://�û����¡���������ʱ����  
        case SB_THUMBPOSITION://�����ק���鲢�ͷ�ʱ��������ʱnPos��Ч  
        case SB_THUMBTRACK://�����ק����ʱ��������ʱnPos��Ч  
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
        case SB_PAGELEFT://Ӱ�������һ�λ�����û�����PageUp��PageDown���������ƶ��ľ��룬��SetPageSize����  
        case SB_PAGERIGHT:  
        case SB_LEFT://�û�����Home������  
        case SB_RIGHT://�û�����End������  
        case SB_LINELEFT://�û����¡���������ʱ����  
        case SB_LINERIGHT://�û����¡���������ʱ����  
        case SB_THUMBPOSITION://�����ק���鲢�ͷ�ʱ��������ʱnPos��Ч  
        case SB_THUMBTRACK://�����ק����ʱ��������ʱnPos��Ч  
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
        case SB_PAGELEFT://Ӱ�������һ�λ�����û�����PageUp��PageDown���������ƶ��ľ��룬��SetPageSize����  
        case SB_PAGERIGHT:  
        case SB_LEFT://�û�����Home������  
        case SB_RIGHT://�û�����End������  
        case SB_LINELEFT://�û����¡���������ʱ����  
        case SB_LINERIGHT://�û����¡���������ʱ����  
        case SB_THUMBPOSITION://�����ק���鲢�ͷ�ʱ��������ʱnPos��Ч  
        case SB_THUMBTRACK://�����ק����ʱ��������ʱnPos��Ч  
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
//��ʾ�豸��Ϣ
void CLionSDKDemoDlg::ShowDeviceInfo(PLU_DEVICE pDevice)
{
	if(pDevice->uvcIdentity.Id > 0)
	{
		WCHAR wcName[128] = { 0 }; 
		WCHAR wcSerial[128] = { 0 };
		mbstowcs(wcName, pDevice->uvcIdentity.Name, 128);
		mbstowcs(wcSerial, pDevice->uvcIdentity.DevSerial, 128);
		CString csText;
		csText.Format(_T("�豸ID: %d \r\n�豸����: %s \r\n�豸���к�: %s\r\n�豸�汾:%d.%d"), 
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
		csText.Format(_T("�豸ID: %d \r\nManufacturer: %s \r\nProductFamily: %s\r\nProductName: %s\r\nЭ��汾:%d.%d"), 
			pDevice->twIdentity.Id, wcManufacturer, wcProductFamily, 
			wcProductName, pDevice->twIdentity.ProtocolMajor, pDevice->twIdentity.ProtocolMinor);
		m_stDeviceInfo.SetWindowText(csText);
	}
}
//�豸ͼ�����
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


//��ȡ�豸״̬
void CLionSDKDemoDlg::OnBnClickedButtonGetstate()
{
	// TODO: Add your control notification handler code here
	HTREEITEM hTree = m_treeDevice.GetSelectedItem();
	if(hTree == NULL)
		return;
	PLU_DEVICE pDevice =(PLU_DEVICE)m_treeDevice.GetItemData(hTree);
	if(pDevice)
	{
		//��ǰ�豸״ֵ̬
		CString csStateInfo;
		LUDEV_STATE dev_state;
		switch(GetDeviceState(pDevice, &dev_state))
		{
		case LU_NOTSUPPORT:			//��֧��
			csStateInfo = _T("�豸״̬: ��֧��!");
			break;
		case LU_PARAMINVALID:		//��Ч����
			csStateInfo = _T("�豸״̬: ��Ч����!");
			break;
		case LU_SUCCESS:
			{
				switch(dev_state)
				{
				case LUDEVSTATE_UNOPNE:				//�豸δ��
					csStateInfo = _T("�豸״̬: �豸δ��!");
					break;
				case LUDEVSTATE_OPEN:					//�豸��
					csStateInfo = _T("�豸״̬: �豸��!");
					break;
				case LUDEVSTATE_WAITTRIGGER:					//�ȴ�����
					csStateInfo = _T("�豸״̬: �ȴ�����!");
					break;
				case LUDEVSTATE_TRIGGERIMAGE:					//������ȡͼ������
					csStateInfo = _T("�豸״̬: ������ȡͼ������!");
					break;
				case LUDEVSTATE_IMAGESAVE:					//ͼ�񱣴�
					csStateInfo = _T("�豸״̬: ͼ�񱣴�!");
					break;
				case LUDEVSTATE_OVERTIME:					//��ʱ
					csStateInfo = _T("�豸״̬: ��ȡͼ��ʱ");
					break;
				}
			}
			break;
		default:
					csStateInfo = _T("�豸״̬: �޷���ȡ!");
			break;
		}
		//
		m_stStateInfo.SetWindowText(csStateInfo);
		//

	}
}

//�жϻ�ȡͼ��
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

//ͬ����ʽ��ȡ������������̻߳�ȡͼ�񣬲��������߳�
void CLionSDKDemoDlg::OnBnClickedButtonSynchronous()
{
	// TODO: Add your control notification handler code here
	//ͬ����ȡͼ�񣬻����������̣߳������ڹ����߳��л�ȡ��
	CreateThread(0, 0, GetImageThreadProc, this, 0, 0);
	
}

//�첽��ʽ��ȡ�ж�
void CLionSDKDemoDlg::OnBnClickedButtonAsynchronous()
{
	// TODO: Add your control notification handler code here
	//�첽��ʽһ
	////��ʱ�ڽ����߳��У������첽��ȡ�������������̣߳��������첽��ȡͼ��һ���ǻ����������̣߳����Զ�β��ԣ����鿪��һ���µĹ����̴߳���
	//if(m_hAsyThread != NULL)
	//{
	//	//��ǰ�����߳������У���Ҫ�Ƚ������ٿ����µġ�
	//	return;
	//}
	//m_hAsyThread = CreateThread(0, 0, AsynchronousGetImageThreadProc, this, 0, 0);
	//if(m_hAsyThread == NULL)
	//{
	//	MessageBox(_T("�������첽�̴߳���ʧ��!!!"));
	//	return;
	//}

	//�첽��ʽ��
	//�첽��ʽ��ȡͼƬ
	HTREEITEM hTree = m_treeDevice.GetSelectedItem();
	PLU_DEVICE pDevice =(PLU_DEVICE)m_treeDevice.GetItemData(hTree);
	if(pDevice)
	{
		//
		m_btnAsynchronous.EnableWindow(FALSE);
		m_btnSynchronous.EnableWindow(FALSE);
		//��ȡͼ�񣬷���ͼ���ļ���
		GetImage(pDevice, 0, ImageCallback);
	}
	
}



/***********************************************************************************************
*�����첽��ȡͼ��
***********************************************************************************************/
//�����첽������ȡͼ���߳�
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
	//�ر��߳̾������ա�
	CloseHandle(pDlg->m_hAsyThread);
	pDlg->m_hAsyThread = NULL;
	return 0;
}
//��ȡͼ��
void CLionSDKDemoDlg::AsynchronousGetImage()
{
	//�첽��ʽ��ȡͼƬ
	HTREEITEM hTree = m_treeDevice.GetSelectedItem();
	PLU_DEVICE pDevice =(PLU_DEVICE)m_treeDevice.GetItemData(hTree);
	if(pDevice)
	{
		//����ͬ�����
		m_hAsyEvent = CreateEvent(NULL, FALSE, FALSE, NULL);
		if(m_hAsyEvent == NULL)
		{
			//����ʧ�ܣ��˳�
			return;
		}
		//
		m_btnAsynchronous.EnableWindow(FALSE);
		m_btnSynchronous.EnableWindow(FALSE);
		m_btnAbandon.EnableWindow(FALSE);					//���жϰ�ťҲʧ��,���������У��˰�ťû��ʵ�����á�
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
		
		//��λ
		ResetEvent(m_hAsyEvent);
		m_nFile = 0;
		memset(m_cFile, 0, sizeof(m_cFile));
		//��ȡͼ�񣬷���ͼ���ļ���
		GetImage(pDevice, 0, ImageCallback);
		//
		int nTm = 20000;
		//
		DWORD dwObj = WaitForSingleObject(m_hAsyEvent, nTm);
		if(dwObj == WAIT_OBJECT_0)
		{
			//�������,��ȡ��ͼ���ļ���
			if (m_nFile > 0 && strlen(m_cFile) == m_nFile)
			{
				//ת���ɿ��ַ���
				MultiByteToWideChar(936, 0, m_cFile, m_nFile, wcFile, 256);
				//����ͼ���ļ�����ʾ����
				hresult1 = img.Load(wcFile);
				m_stImage.GetClientRect(rect);
				if (E_FAIL == hresult1)
				{
					MessageBox(_T(" m_image Load  failed!"));
					return;
				}
				img.Draw(m_stImage.GetDC()->m_hDC, rect);
				csStateInfo = _T("�豸״̬: ��ͼ���! �壺 ");
				//TRACE("�豸״̬: ��ͼ���\r\n");
				img.Destroy();

			}
			else
			{
				TRACE("�豸״̬: ��ͼ����\r\n");
				csStateInfo = _T("�豸״̬: ��ͼ����! �壺 ");
			}
		}
		else
		{
			//��ʱ��ֱ��ʹ�ýӿڴ���
			AbandonGetImage(pDevice);
			//�ٴεȴ�����Ƿ����
			dwObj = WaitForSingleObject(m_hAsyEvent, 10);
			if(dwObj == WAIT_OBJECT_0)
			{
				//����ɣ���Ҫ�жϣ���ͼ���ȡ��ɣ�����ȡ�����,
				if (m_nFile > 0 && strlen(m_cFile) == m_nFile)
				{
					//������ȡ�ɹ���
					TRACE("�豸״̬: ȡ����������ͼ���!\r\n");
					csStateInfo = _T("�豸״̬: ȡ����������ͼ���! �壺 ");
				}
				else
				{
					//ȡ���ɹ�
					TRACE("�豸״̬: ȡ��������ȡ���ɹ�!\r\n");
					csStateInfo = _T("�豸״̬: ȡ��������ȡ���ɹ�! �壺 ");
				}
			}
			else
			{
				//��ʱ��ȡ��������δ��ɡ�
				TRACE("�豸״̬: ȡ��������ȡ����ʱ!\r\n");
				csStateInfo = _T("�豸״̬: ȡ��������ȡ����ʱ! �壺 ");
			}
		}
		m_stStateInfo.SetWindowText(csStateInfo);

		//�ͷ�
		CloseHandle(m_hAsyEvent);
		m_hAsyEvent = NULL;
		//ʹ��
		m_btnAsynchronous.EnableWindow(TRUE);
		m_btnSynchronous.EnableWindow(TRUE);
		m_btnAbandon.EnableWindow(TRUE);
	}
}


LU_RESULT CLionSDKDemoDlg::ImageCallback(LU_DEVICE device, LU_PCHAR pImgData, LU_UINT32 nDataBuf, LU_PCHAR pFile, LU_UINT32 nFileBuf)
{
	
	////��ʽһ��ͨ���߳��첽���á�
	//TRACE("ImageCallBack\r\n");
	////����ͼ���ļ����Ƴ���
	//pSDKDemoDlg->m_nFile = nFileBuf;
	////����ͼ���ļ�����
	//if(pSDKDemoDlg->m_nFile > 0)
	//	memcpy(pSDKDemoDlg->m_cFile, pFile, nFileBuf);
	////ͼ���ȡ���
	//if(pSDKDemoDlg->m_hAsyEvent != NULL)
	//	SetEvent(pSDKDemoDlg->m_hAsyEvent);

	//��ʽ��ͨ�������̣߳��첽����
	pSDKDemoDlg->ShowImageData(device, pImgData, nDataBuf, pFile, nFileBuf);

	return 0;
}


