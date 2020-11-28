function VisibleSideMenu() {
        var SideDiver = document.getElementById('div_sideMenu');
        var DivSide = document.getElementById('SideMenu');
        let DivSideStyle = window.getComputedStyle(DivSide);
        let DivSideDisplay = DivSideStyle.getPropertyValue('width');
    
    if (DivSideDisplay === '0px') {
        SideDiver.style.display = 'block';
        setTimeout(
            () => {
                DivSide.style.width = '200px'
            }
        ,100)
    } else {
        DivSide.style.width = '0px';
        setTimeout(
            () => {
                SideDiver.style.display = 'none'
            }
        , 500)
        }
};

function VisibleMenuDocumentos() {
    var SideDiver = document.getElementById('div_menu_documentos');
    var DivSide = document.getElementById('menu_documentos');
    let DivSideStyle = window.getComputedStyle(DivSide);
    let DivSideDisplay = DivSideStyle.getPropertyValue('width');

    if (DivSideDisplay === '0px') {
        SideDiver.style.display = 'block';
        setTimeout(
            () => {
                DivSide.style.width = '200px';
            }
            , 100)
    } else {
        console.log('funcionax')
        DivSide.style.width = '0px';
        setTimeout(
            () => {
                SideDiver.style.display = 'none'
            }
            , 500)
    }
};
