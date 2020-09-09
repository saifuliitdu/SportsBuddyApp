var app = {
    upperCase: new RegExp('[A-Z]'),
    lowerCase: new RegExp('[a-z]'),
    numbers: new RegExp('[0-9]'),
    specialKey: new RegExp('!@#$%^&./\\><;:"|~()+-/*'),
    currentSelection: {},
    dropboxFileList: [],
    boxFileList: [],

    init: function () {
        var _this = this;
        //var timezoneoffset = (new Date()).getTimezoneOffset() / -60;
        //_this.setCookie("ClientTimeZoneOffset", timezoneoffset.toString(), 30);
        //document.cookie = `ClientTimeZoneOffset=${Date.getTimezoneOffset() / 60}; SameSite=None; Secure`;
        $(document).keyup(function (e) {
            if (e.keyCode == 27) { // escape key maps to keycode `27`
                _this.closePopup();
            }
        });
        // _this.toastSetup();
        //$("#user-mobile").intlTelInput({
        //    initialCountry: "auto",
        //    geoIpLookup: function (callback) {
        //        $.get("https://ipinfo.io", function () { }, "jsonp").always(function (resp) {
        //            var countryCode = (resp && resp.country) ? resp.country : "";
        //            callback(countryCode);
        //        });
        //    },
        //    utilsScript: "../lib/intl-tel-input/js/utils.js"
        //});
    },
    
    signUp: function () {
        var _this = app;
        var data = {
            firstName: $("#FirstName").val(),
            lastName: $("#LastName").val(),
            regEmail: $("#RegEmail").val(),
            regPassword: $("#RegPassword").val(),
            confirmPassword: $("#ConfirmPassword").val()
        };
        $("#register-form").find(".form-control").each(function () {
            if ($(this).val() == "") {
                $(this).addClass("red-border");
            } else {
                $(this).removeClass("red-border");
            }
        });
        if (!data.regPassword.match(app.upperCase) || !data.ConfirmPassword.match(app.upperCase)) {
            if (!data.regPassword.match(app.upperCase)) {
                $("#RegPassword").addClass("red-border");
            } if (!data.ConfirmPassword.match(app.upperCase)) {
                $("#ConfirmPassword").addClass("red-border");
            }
            $("#register-error").show().text("Password length must be 6 containing at least one number, an uppercase and a lowercase letter.");
            return;
        }
        if (!data.regPassword.match(app.lowerCase) || !data.ConfirmPassword.match(app.lowerCase)) {
            if (!data.regPassword.match(app.lowerCase)) {
                $("#RegPassword").addClass("red-border");
            } if (!data.ConfirmPassword.match(app.lowerCase)) {
                $("#ConfirmPassword").addClass("red-border");
            }
            $("#register-error").show().text("Password length must be 6 containing at least one number, an uppercase and a lowercase letter.");
            return;
        }

        if (data.ConfirmPassword.length < 6 || data.regPassword.length < 6) {
            if (data.ConfirmPassword.length < 6) {
                $("#RegPassword").addClass("red-border");
            } if (data.regPassword.length < 6) {
                $("#ConfirmPassword").addClass("red-border");
            }
            $("#register-error").show().text("Password length must be 6 containing atleast one number, uppercase, lowercase letter and a special character.");
            return;
        }
        else {
            $("#ConfirmPassword").removeClass("red-border");
            $("#RegPassword").removeClass("red-border");
        }
        $("#reg-error,#register-error").hide();
        if (!validateEmail(data.regEmail)) {
            $("#RegEmail").addClass("red-border");
        }
        if (data.ConfirmPassword != data.regPassword) {
            $("#register-error").show().find('p').text("Password mismatch.");
            $("#ConfirmPassword").addClass("red-border");
            $("#RegPassword").addClass("red-border");
            return;
        }
        if ($("#register-form").find(".red-border").length <= 0) {
            $.ajax({
                url: '/Account/Register',
                type: 'POST',
                data: data,
                success: function (res) {
                    if (res.status) {
                        $("#register-form,#reg-error,#myTab,#thitd-party-logins,#or-text").hide();
                        $("#reg-success").show().find('p').text(res.msg);
                    }
                    else {
                        $("#register-form,#reg-success,#myTab,#thitd-party-logins,#or-text").hide();
                        $("#reg-error").show().find('p').text(res.msg);
                    }
                }
            });
        } else {
            return;
        }
    },
    
    
    saveProfile: function () {
        var _this = app;
        var isValid = $("#user-mobile").intlTelInput("isValidNumber");
        if ($("#user-mobile").val() == "" || !isValid) {
            $("#user-mobile").addClass("red-border");
            return;
        } else {
            $("#user-mobile").removeClass("red-border");
        }
        var url = '/Profile/SaveProfile';
        var profile = {
            Mobile: $('#user-mobile').intlTelInput("getNumber"),
            CompanyName: $('#company-name').val(),
            OfficeAddress: $('#offcie-address').val(),
            CompanyWebsite: $('#company-website').val(),
            OthersMemberEmail: $('#other-email').val()
        };
        $("#error-msg").hide();
        $.ajax({
            type: "POST",
            url: url,
            data: { profile: profile },
            success: function (data) {
                if (data === 'success') {
                    // $("#circle").hide();
                    $("#error-msg").show().css("color", "green");
                    $("#error-msg").text("Successfully Updated.");
                    setTimeout(function () {
                        location.href = "/";
                    }, 2000)
                } else if (data == 'pass') {
                    $("#circle").hide();
                    $("#error-msg").show().css("color", "green").text("Successfully Updated.")
                    setTimeout(function () {
                        window.location.href = "/Account/Login";
                    }, 4000);

                } else if (data == 'error') {
                    // $("#circle").hide();
                }
                else {
                    $("#error-msg").show().css("color", "red");
                    $("#error-msg").text(data);
                }
            }, error: function () {
                // $("#circle").hide();
            }
        });
    },
    updatePassword: function () {
        var url = '/Profile/updatePassword';
        var update = {
            NewPassword: $("#newPassword").val(),
            Password: $("#password").val(),
            ConfirmPassword: $("#confirmPassword").val(),
        };
        if (update.Password != "") {
            $("#password").removeClass("red-border");
            if (update.NewPassword == update.ConfirmPassword) {
                $("#circle").show();
                $('.second.circle').circleProgress({
                    value: 1, fill: { color: '#08D7AD' }
                }).on('circle-animation-progress', function (event, progress) {
                    $(this).find('strong').html(Math.round(100 * progress) + '<i>%</i>');
                });
                $.ajax({
                    type: "POST",
                    url: url,
                    data: update,
                    success: function (data) {
                        $("#circle").hide();
                        if (data) {
                            $("#error-msg").show().css("color", "green").text("Successfully Updated.")
                            setTimeout(function () {
                                window.location.href = "/Account/Login";
                            }, 4000);
                        } else {

                            $("#error-msg").show().css("color", "red").text("Update failed.")
                        }
                    }, error: function () {
                        $("#loadingDiv").hide();
                    }
                });
            } else {
                $("#confirmPassword,#newPassword").addClass("red-border");
                return;
            }
        } else {
            $("#password").addClass("red-border");
            return;
        }
    },
    hideProfile: function () {
        var _this = app;
        $("#profile-pass,#passUpdate").addClass("animated").addClass("slideInUp").toggle();
        $("#profile-info,#profile-update").toggle();
    },
    validateEmail: function (Email) {
        var pattern = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
        return $.trim(Email).match(pattern) ? true : false;
    },
    validatePassword: function (pass) {
        var pattern = /[^\w\s]/gi;
        return $.trim(pass).match(pattern) ? true : false;
    },
    toastSetup: function () {
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "5000",
            // "hideDuration": "2000",
            // "timeOut": "5000",
            "extendedTimeOut": "5000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };
    },
    login: function (element) {
        console.log("clicked");
        var _this = app;
        if ($("#login-email").val() == "" || !_this.validateEmail($("#login-email").val()) || $("#login-password").val() == "") {
            if ($("#login-email").val() == "" || !_this.validateEmail($("#login-email").val())) {
                $("#login-email").addClass("red-border");
            } else {
                $("#login-email").removeClass("red-border");
            }
            if ($("#login-password").val() == "") {
                $("#login-password").addClass("red-border");
            }
            else {
                $("#login-password").removeClass("red-border");
            }
            return;
        }
        var data = {
            email: $("#login-email").val(),
            password: $("#login-password").val()
        };

        $.ajax({
            url: '/Account/Login',
            method: 'POST',
            data: data,
            success: function (res) {
                if (res.status == true) {
                    location.href = res.message;
                }
                if (res.status == false) {
                    toastr["error"](res.message);
                }
            }
        });
    },
    showLogin: function (element) {
        $(element).hide();
        $("#sign-in-form,#myTab").show();
        $("#forgot-area").hide().find('p').remove();
        $("#modal-cross").hide();
    },
   
    yesRadio: function (element) {
        if ($(element).is(":checked")) {
            $("#feedback").hide();
        }
    },
    noRadio: function (element) {
        if ($(element).is(":checked")) {
            $("#feedback").show();
            $(".compare-popup").scrollTop(300, "slow");
        }
    },
    
    closePopup: function (element) {
        var _this = element;
        $(".image-popup").empty().hide();
        $('img').removeClass("current");
    },
   
    tabClick: function (evt, tabName) {
        previousSelected = 0;
        var i, tabcontent, tablinks;
        tabcontent = document.getElementsByClassName("tabcontent");
        for (i = 0; i < tabcontent.length; i++) {
            tabcontent[i].style.display = "none";
        }
        tablinks = document.getElementsByClassName("tablinks");
        for (i = 0; i < tablinks.length; i++) {
            tablinks[i].className = tablinks[i].className.replace(" active", "");
        }
        document.getElementById(tabName).style.display = "block";
        evt.currentTarget.className += " active";
    },
    setCookie: function (cname, cvalue, exdays) {
        var d = new Date();
        d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        document.cookie = cname + "=" + cvalue + "; SameSite=None; Secure";
    },
    getCookie: function (cname) {
        var name = cname + "=";
        var decodedCookie = decodeURIComponent(document.cookie);
        var ca = decodedCookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) == 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    }
};
$(document).ready(function () {
    app.init();
});

$(document).ajaxStart(function () {
    $("#loader").show();
}).ajaxStop(function () {
    $("#loader").hide();
});