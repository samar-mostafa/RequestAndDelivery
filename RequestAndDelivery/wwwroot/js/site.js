function getBranchDepartments(id, dep) {

    $.get({
        url: "/Requests/GetBranchDepartments/" + id,
        success: function (data) {
            var html = "<option value=0 selected>أختر الاداره</option>";
            $.each(data, function (key, value) {
                html += `<option value=${value.id}>${value.name}</option>`;
                $(dep).html(html);
            })

        }
    })

}

function getEmployeeData(inp) {
    var input = $(inp);
    var value = input.val();
    $.get({
        url: "/Requests/GetEmployeeDataForJson/" + value,
        success: function (data) {
            console.log(data)
            $(inp).closest("div.empData").find("input.empName").prop("value", data.name);
            var option = $('<option></option>').attr("value", data.branchData.value).text(data.branchData.text);
            $(inp).closest("div.empData").find(".empBranch").html(option);
            var option2 = $('<option></option>').attr("value", data.departmentData.value).text(data.departmentData.text);
            $(inp).closest("div.empData").find(".empDepart").html(option2);
            // $(inp).closest("div.empData").find("input.empBranch").prop("value", data.name);
            //$(inp).closest("div.empData").find("input[name='EmployeeDeliverToName']").prop("value", data.name);
        }
    });
}




$(document).ready(function () {
    //select2
    $('.js-select2Single').select2();
    document.querySelectorAll('.nav-link').forEach((link) => {
        if (link.href == window.location.href) {
            link.classList.add("active")
        }
    });
    $('body').delegate(('.js-model-render'), 'click', function () {

        var btn = $(this);
        var modelTitle = btn.data('title');
        var url = btn.data('url');
        var model = $('#Model');
       

        $.get({
            url: url,
            success: function (form) {
                model.find('#ModelLabel').text(modelTitle);
                model.find('.modal-body').html(form);
/*                $.validator.unobtrusive.parse(model);*/
                window.$('#Model').modal('show');
            },
            error: function () {
                showErrorMessage();
            }

        });
    });
})

window.addEventListener('DOMContentLoaded', event => {

    // Navbar shrink function
    var navbarShrink = function () {
        const navbarCollapsible = document.body.querySelector('#mainNav');
        if (!navbarCollapsible) {
            return;
        }
        if (window.scrollY === 0) {
            navbarCollapsible.classList.remove('navbar-shrink')
        } else {
            navbarCollapsible.classList.add('navbar-shrink')
        }

    };

    // Shrink the navbar 
    navbarShrink();

    // Shrink the navbar when page is scrolled
    document.addEventListener('scroll', navbarShrink);

    // Activate Bootstrap scrollspy on the main nav element
    const mainNav = document.body.querySelector('#mainNav');
    if (mainNav) {
        new bootstrap.ScrollSpy(document.body, {
            target: '#mainNav',
            rootMargin: '0px 0px -40%',
        });
    };

    // Collapse responsive navbar when toggler is visible
    const navbarToggler = document.body.querySelector('.navbar-toggler');
    const responsiveNavItems = [].slice.call(
        document.querySelectorAll('#navbarResponsive .nav-link')
    );
    responsiveNavItems.map(function (responsiveNavItem) {
        responsiveNavItem.addEventListener('click', () => {
            if (window.getComputedStyle(navbarToggler).display !== 'none') {
                navbarToggler.click();
            }
        });
    });
});