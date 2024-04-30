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
- WPF 기본학습
    - 데이터바인딩 마무리
    - 디자인 리소스

- WPF MVVM