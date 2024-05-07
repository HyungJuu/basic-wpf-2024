# basic-wpf-2024
부경대 2024 IoT 개발자과정 WPF 학습 리포지토리

## 1일차 (24.04.29)
- WPF(Window Presentation Foundation) 기본학습
    - Winforms 확장한 WPF
        - 이전의 Winforms 이미지 : 비트맵방식 -> 확대시 이미지 깨짐
        - WPF 이미지 -> 벡터방식
        - XAML 화면 디자인 - 안드로이드 개발시 Java XML로 화면디자인과 PyQt로 디자인과 동일

    - XAML(엑스에이엠엘, 재믈)
        - 여는 태그 : <Window>
        - 닫는 태그 : </Window>
        - 합치면 : <Window /> -> Window 태그 안에 다른객체가 없다는 뜻
        - 여는 태그와 닫는 태그 사이에 다른 태그(객체)를 넣어서 디자인

    - WPF 기본 사용법
        - Winforms와는 다르게 코딩으로 디자인을 함

    - 레이아웃
        1. Grid : WPF에서 가장 많이 쓰는 대표적인 레이아웃(중요★)
        2. StackPanel : 스택으로 컨트롤을 쌓는 레이아웃
        3. Canvas : 미술에서의 캔버스와 유사
        4. DockPanel : 컨트롤을 방향에 따라서 도킹시키는 레이아웃
        5. Margin : 여백기능, 앵커링 같이함(중요★)


## 2일차 (24.04.30)
- WPF 기본학습
    - 데이터바인딩 : 데이터소스(DB, 엑셀, txt, 클라우드에 보관된 데이터의 원본)의 데이터를 쉽게 가져다쓰기 위한 데이터 핸들링방법
        - xaml : {Binging Path=속성, ElememtName=객체, Mode=(OneWay | TwoWay), StringFormat={} {0:#,#}}
        - dataContext : 데이터를 담아서 전달하는 이름
        - 전통적인 윈폼 코드비하인드에서 데이터를 처리하는 것을 지양 - 디자인, 개발부분 분리

## 3일차 (24.05.02)
- WPF에서 중요한 개념(윈폼과 다른점)
    1. 데이터바인딩 - 바인딩 키워드로 코드와 분리
    2. 옵저버패턴 - 값이 변경된 사실을 사용자에게 공지 (OnPropertyChanged 이벤트)
    3. 디자인리소스 - 각 컨트롤마다 디자인(X), 리소스로 디자인 공유
        - 각 화면당 Resources : 자기 화면에만 적용되는 디자인
        - App.xaml Resources : 애플리케이션 전체에 적용되는 디자인
        - 리소스 사전 : 공유할 디자인 내용이 많을 때 파일로 따로 지정

- WPF MVVM
    - MVC(Model View Controller 패턴)
        - 웹개발(Spring, ASP.Net, MVC, dJango, etc...) 현재도 사용되고 있음
        - Model : Data입출력 처리를 담당
        - View : 디스플레이 화면 담당
        - Controller : View를 제어, Model 처리 중앙에 관장

    - MVVM(Model View ViewModel)
        - Model : Data 입출력(DB side)
        - View : 화면, 순수 xaml로만 구성
        - ViewModel : 뷰에 대한 메서드, 액션, INotifyPropertyChanged를 구현 

        ![MVVM패턴](https://raw.githubusercontent.com/HyungJuu/basic-wpf-2024/main/images/wpf001.png)

    - 권장 구현방법
        - ViewModel 생성, 알림 속성 구현
        - View에 ViewModel을 데이터바인딩
        - Model DB작업을 독립적으로 구현

    - MVVM 구현을 도와주는 프레임워크
        0. ~~Mvvmlight.Toolkit~~ : 3rd Party 개발. 2009년부터 시작 ~ 2014년도 이후로 더이상 개발이나 지원이 없음
        1. **Caliburn.Micro** : MVVM 구현이 아주 간단하고 강력함.중소형 프로젝트에 적합. But, 디버깅이 조금 어려움
        2. AvaloniaUI : 3rd Party 개발. 크로스플랫폼. 디자인은 최고
        3. Prism : Microsoft 개발. 매우 어려움. 대규모 프로젝트 활용

- Caliburn.Micro
    1. 프로젝트 생성 후 MainWindow.xaml 삭제
    2. Models, Views, ViewModels 폴더(네임스페이스) 생성
    3. 종속성 NuGet패키지 Caliburn.Micro 설치
    4. 루트폴더에 Bootstrapper.cs 클래스 생성, 작성
    5. App.xaml에서 StartupUri 삭제
    6. App.xaml에 Bootstrapper 클래스를 리소스사전에 등록
    7. App.xaml.cs에 App() 생성자 추가
    8. ViewModels 폴더에 MainViewModel.cs 클래스 생성
    9. Bootstrapper.cs에 OnStartup() 내용을 변경
    10. Views 폴더에 MainViewModel 생성

    - 작업 분리
        - DB 개발자 : DBMS 테이블 생성, Models에 클래스 작업
        - Xaml 디자이너 : Views 폴더에 있는 xaml 파일을 디자인작업

## 4일차 (24.05.03)
- Caliburn.Micro
    - 작업 분리
        - Xaml 디자이너 : xaml 파일만 디자인
        - ViewModel 개발자 : Model에 있는 DB관련정보와 View를 연계하여 전체구현작업

    - Calburn.Micro 특징
        - Xaml 디자인 시 {Binding ...} &rarr; 잘 사용하지 않음
        - 대신 x:Name을 사용

    - MVVM 특징
        - 예외발생 시 예외메시지의 표시 없이 프로그램 종료
        - ViewModel에서 디버깅 시작
        - View.xaml 바인딩, 버튼클릭 등의 이름 지정 주의
        - Model 내 DB테이블 컬럼 이름 일치, CRUD 쿼리문 오타 주의
        - ViewModel 부분
            - 변수, 속성으로 분리
            - 속성이 Model 내의 속성과 이름이 일치
            - List 사용불가 -> BindableCollection으로 변경
            - 메서드와 이름이 동일한 Can... 프로퍼티 지정, 버튼 활성/비활성화
            - 모든 속성에 NotifyOfPropertyChange() 메서드 존재!!(값 변경 알림)

    ![실행화면](https://raw.githubusercontent.com/HyungJuu/basic-wpf-2024/main/images/wpf002.png)

## 5일차 (24.05.07)
- MahApps.Metro https://mahapps.com/
    - Metro(Modern UI) 디자인 접목 http://materialdesigninxaml.net/         

    ![실행화면](https://raw.githubusercontent.com/HyungJuu/basic-wpf-2024/main/images/wpf003.png)

    ![저장화면](https://raw.githubusercontent.com/HyungJuu/basic-wpf-2024/main/images/wpf004.png)

- Movie API 연동 앱, MovieFinder 2024
    - DB(SQL Server) 연동
    - MahApps.Metro
    - OpenAPI 두가지 사용
    - MVVM은 시간부족으로 사용안함

    - 좋아하는 영화 즐겨찾기 앱
    - [TMDB](https://www.themoviedb.org/) OpenAI 활용
        - 회원가입 후 API 신청

    - [Youtube API](https://console.cloud.google.com/) 활용
        - 새 프로젝트 생성
        - API 및 서비스, 라이브러리 선택
        - Youtube Data API v3 선택, 사용버튼 클릭
        - 사용자 인증정보 만들기 클릭
            1. 사용자 데이터 라디오버튼 클릭
            2. OAutho 동의화면, 기본내용 입력 후 다음
            3. 범위는 저장 후 계속
            4. OAutho Client ID, 앱유형 : 데스크톱앱, 이름 입력 후 만들기 클릭
