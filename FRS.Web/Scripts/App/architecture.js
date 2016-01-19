// Global Variable
var ist = {
    datePattern: "DD/MM/YY",
    shortDatePattern: "dd-M-yy",
    customShortDatePattern: "dd-mm-yy",
    customShortDateWithFullYearPattern: "dd-mm-yyyy",
    customLongDateWithFullYearPattern: "DD-MM-YYYY",
    timePattern: "HH:mm",
    hourPattern: "HH",
    minutePattern: "mm",
    dateTimePattern: "DD/MM/YY HH:mm",
    customDateTimePattern: "DD/MM/YYYY HH:mm",
    dateTimeWithSecondsPattern: "DD/MM/YY HH:mm:ss",
    // UTC Date Format
    utcFormat: "YYYY-MM-DDTHH:mm:ss",
    numberFormat: "0,0.00",
    ordinalFormat: "0",
    zeroDecimalNumberFormat: "0,0",
    //server exceptions enumeration 
    exceptionType: {
        CaresGeneralException: 'CaresGeneralException',
        UnspecifiedException: 'UnspecifiedException'
    },
    //verify if the string is a valid json
    verifyValidJSON: function (str) {
        try {
            JSON.parse(str);
        } catch (exception) {
            return false;
        }
        return true;
    },
    // Validate Url
    validateUrl: function (field) {
        var regex = /^(?:(?:https?|ftp):\/\/)(?:\S+(?::\S*)?@)?(?:(?!10(?:\.\d{1,3}){3})(?!127(?:\.\d{1,3}){3})(?!169\.254(?:\.\d{1,3}){2})(?!192\.168(?:\.\d{1,3}){2})(?!172\.(?:1[6-9]|2\d|3[0-1])(?:\.\d{1,3}){2})(?:[1-9]\d?|1\d\d|2[01]\d|22[0-3])(?:\.(?:1?\d{1,2}|2[0-4]\d|25[0-5])){2}(?:\.(?:[1-9]\d?|1\d\d|2[0-4]\d|25[0-4]))|(?:(?:[a-z\u00a1-\uffff0-9]+-?)*[a-z\u00a1-\uffff0-9]+)(?:\.(?:[a-z\u00a1-\uffff0-9]+-?)*[a-z\u00a1-\uffff0-9]+)*(?:\.(?:[a-z\u00a1-\uffff]{2,})))(?::\d{2,5})?(?:\/[^\s]*)?$/i;
        return (regex.test(field)) ? true : false;
    },
    // Resource Text
    resourceText: {},
    // SiteUrl
    siteUrl: ""
};

// Busy Indicator
var spinnerVisibleCounter = 0;

// Show Busy Indicator
function showProgress() {
    ++spinnerVisibleCounter;
    if (spinnerVisibleCounter > 0) {
        $("div#spinner").fadeIn("fast");
    }
};

// Hide Busy Indicator
function hideProgress() {
    --spinnerVisibleCounter;
    if (spinnerVisibleCounter <= 0) {
        spinnerVisibleCounter = 0;
        var spinner = $("div#spinner");
        spinner.stop();
        spinner.fadeOut("fast");
    }
};


//status decoder for parsing the exception type and message
amplify.request.decoders = {
    istStatusDecoder: function (data, status, xhr, success, error) {
        if (status === "success") {
            success(data);
        } else {
            if (status === "fail" || status === "error") {
                var errorObject = {};
                errorObject.errorType = ist.exceptionType.UnspecifiedException;
                if (ist.verifyValidJSON(xhr.responseText)) {
                    errorObject.errorDetail = JSON.parse(xhr.responseText);;
                    if (errorObject.errorDetail.ExceptionType === ist.exceptionType.CaresGeneralException) {
                        error(errorObject.errorDetail.Message, ist.exceptionType.CaresGeneralException);
                    } else {
                        error("Unspecified exception", ist.exceptionType.UnspecifiedException);
                    }
                } else {
                    error(xhr.responseText);
                }
            } else if (status === "nocontent") { // Added by ali : nocontent status is returned when no response is returned but operation is sucessful
                success(data);

            } else {
                error(xhr.responseText);
            }
        }
    }
};

// If while ajax call user shifts to another page then avoid error toasts
amplify.subscribe("request.before.ajax", function (resource, settings, ajaxSettings, ampXhr) {
    // ReSharper disable InconsistentNaming
    var _error = ampXhr.error;
    // ReSharper restore InconsistentNaming

    function error(data, status) {
        _error(data, status);
    }

    ampXhr.error = function (data, status) {
        if (ampXhr.status === 0) {
            return;
        }
        error(data, status);
    };

});

// Knockout Validation + Bindings

var ko = window["ko"];

require(["ko", "knockout-validation"], function (ko) {
    ko.utils.stringStartsWith = function (string, startsWith) {
        string = string || "";
        if (startsWith.length > string.length)
            return false;
        return string.substring(0, startsWith.length) === startsWith;
    };

    ko.validation.rules['url'] = {
        validator: function (val, required) {
            if (!val) {
                return !required;
            }
            val = val.replace(/^\s+|\s+$/, ''); //Strip whitespace
            return val.match(/^(?:(?:https?|ftp):\/\/)(?:\S+(?::\S*)?@)?(?:(?!10(?:\.\d{1,3}){3})(?!127(?:\.‌​\d{1,3}){3})(?!169\.254(?:\.\d{1,3}){2})(?!192\.168(?:\.\d{1,3}){2})(?!172\.(?:1[‌​6-9]|2\d|3[0-1])(?:\.\d{1,3}){2})(?:[1-9]\d?|1\d\d|2[01]\d|22[0-3])(?:\.(?:1?\d{1‌​,2}|2[0-4]\d|25[0-5])){2}(?:\.(?:[1-9]\d?|1\d\d|2[0-4]\d|25[0-4]))|(?:(?:[a-z\u00‌​a1-\uffff0-9]+-?)*[a-z\u00a1-\uffff0-9]+)(?:\.(?:[a-z\u00a1-\uffff0-9]+-?)*[a-z\u‌​00a1-\uffff0-9]+)*(?:\.(?:[a-z\u00a1-\uffff]{2,})))(?::\d{2,5})?(?:\/[^\s]*)?$/i);
        },
        message: 'This field has to be a valid URL!'
    };
    ko.validation.registerExtenders();
    ko.bindingHandlers.editor = {
        init: function (element, valueAccessor) {
            var value = valueAccessor();
            var valueUnwrapped = ko.unwrap(value);
            var $element = $(element);
            CKEDITOR.basePath = ist.siteUrl + "/RichTextEditor/";
            //var myinstance = CKEDITOR.instances['content'];  // services 
            //var myinstance1 = CKEDITOR.instances['content1']; // about us 
            //check if my instance already exist
            //if (myinstance !== undefined) {
            //    CKEDITOR.remove(myinstance);
            //}
            CKEDITOR.config.toolbar = 'Full';
            CKEDITOR.replace(element).setData(valueUnwrapped || $element.html());

            var instance = CKEDITOR.instances['ServiceContentEng'];   // services
            //var instance1 = CKEDITOR.instances['content1']; // abuot us 
            if (ko.isObservable(value)) {
                var isEditorChange = true;
                $element.html(value());
                $.fn.modal.Constructor.prototype.enforceFocus = function () {
                    var modalThis = this;
                    $(document).on('focusin.modal', function (e) {
                        if (modalThis.$element[0] !== e.target && !modalThis.$element.has(e.target).length
                            // add whatever conditions you need here
                            &&
                            !$(e.target.parentNode).hasClass('cke_dialog_ui_input_select') && !$(e.target.parentNode).hasClass('cke_dialog_ui_input_text')) {
                            modalThis.$element.focus();
                        }
                    });
                };
                // Handles typing changes 
                instance.on('contentDom', function () {
                    instance.document.on('keyup', function (event) {
                        handleAfterCommandExec(event);
                    });
                });


                function handleAfterCommandExec() {

                    if (ist.dashBoard.viewModel.dashboard() !== undefined && ist.dashBoard.viewModel.dashboard() !== null) {
                        if (instance.getData() === ist.dashBoard.viewModel.dashboard().services()) {
                            return;
                        }
                    }

                    if (ist.dashBoard.viewModel.dashboard() !== undefined && ist.dashBoard.viewModel.dashboard() !== null) {
                        if (instance.getData() === ist.dashBoard.viewModel.dashboard().aboutUs()) {
                            return;
                        }
                    }
                }

                // Handles styling changes 
                instance.on('afterCommandExec', handleAfterCommandExec);
                // Handles styling Drop down changes like font size, font family 
                instance.on('selectionChange', handleAfterCommandExec);


                value.subscribe(function (newValue) {
                    if (!isEditorChange) {
                        $element.html(newValue);
                    }
                });
            }
        },
        update: function (element, valueAccessor) {
            //handle programmatic updates to the observable
            var existingEditor = CKEDITOR.instances && CKEDITOR.instances[element.id];
            if (existingEditor) {
                existingEditor.setData(ko.utils.unwrapObservable(valueAccessor()), function () {
                    this.checkDirty(); // true
                });
            }
        }
    };
    // jquery date picker binding. Usage: <input data-bind="datepicker: myDate, datepickerOptions: { minDate: new Date() }" />. Source: http://jsfiddle.net/rniemeyer/NAgNV/
    ko.bindingHandlers.datepicker = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            //initialize datepicker with some optional options
            // ReSharper disable DuplicatingLocalDeclaration
            var options = allBindingsAccessor().datepickerOptions || {};
            options.changeMonth = true;
            options.changeYear = true;
            if (!options.yearRange) {
                options.yearRange = "-100:+10";
            }
            // ReSharper restore DuplicatingLocalDeclaration
            $(element).datepicker(options);
            $(element).datepicker("option", "dateFormat", options.dateFormat || ist.customShortDatePattern);
            // If Min Date is specified
            if (options.minDate) {
                $(element).datepicker("option", "minDate", options.minDate);
            }
            //handle the field changing
            ko.utils.registerEventHandler(element, "change", function () {
                var observable = valueAccessor();
                observable($(element).datepicker("getDate"));
            });

            //handle disposal (if KO removes by the template binding)
            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                $(element).datepicker("destroy");
            });

        },
        update: function (element, valueAccessor) {
            var observable = valueAccessor();

            var value = ko.utils.unwrapObservable(valueAccessor()),
                current = $(element).datepicker("getDate");

            if (value - current !== 0) {
                $(element).datepicker("setDate", value);
            }
            //For showing highlighted field if contains invalid value
            if (observable.isValid) {
                if (!observable.isValid() && observable.isModified()) {
                    $(element).addClass('errorFill');
                } else {
                    $(element).removeClass('errorFill');
                }
            }
        }
    };

    // jquery date time picker binding. Usage: <input data-bind="datetimepicker: myDate, datepickerOptions: { minDate: new Date() }" />. Source: http://jsfiddle.net/rniemeyer/NAgNV/
    ko.bindingHandlers.datetimepicker = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            //initialize datepicker with some optional options
            // ReSharper disable DuplicatingLocalDeclaration
            var options = allBindingsAccessor().datepickerOptions || {};
            options.changeMonth = true;
            options.changeYear = true;
            options.yearRange = "-100:+10";
            // ReSharper restore DuplicatingLocalDeclaration
            $(element).datetimepicker(options);
            $(element).datetimepicker("option", "dateFormat", options.dateFormat || ist.customShortDatePattern);
            //handle the field changing
            ko.utils.registerEventHandler(element, "change", function () {
                var observable = valueAccessor();
                observable($(element).datetimepicker("getDate"));
            });

            //handle disposal (if KO removes by the template binding)
            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                $(element).datetimepicker("destroy");
            });

        },
        update: function (element, valueAccessor) {
            var observable = valueAccessor();

            var value = ko.utils.unwrapObservable(valueAccessor()),
                current = $(element).datetimepicker("getDate");

            if (value - current !== 0) {
                $(element).datetimepicker("setDate", value);
            }
            //For showing highlighted field if contains invalid value
            if (observable.isValid) {
                if (!observable.isValid() && observable.isModified()) {
                    $(element).addClass('errorFill');
                } else {
                    $(element).removeClass('errorFill');
                }
            }
        }
    };

    // jquery calendars picker binding. Usage: <input data-bind="calendarspicker: myDate, calendarspickerOptions: { instance: 'islamic' }" />. Source: Keith Wood
    ko.bindingHandlers.calendarspicker = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            //initialize datepicker with some optional options
            // ReSharper disable DuplicatingLocalDeclaration
            var options = allBindingsAccessor().calendarspickerOptions || {};
            var calendar = $.calendars.instance(options.instance || 'islamic');
            // ReSharper restore DuplicatingLocalDeclaration
            $(element).calendarsPicker({
                calendar: calendar,
                dateFormat: ist.customShortDateWithFullYearPattern,
                onSelect: function () {
                    var observable = valueAccessor();
                    observable($(this).val());
                },
                yearRange: options.yearRange || "-100:+10",
                minDate: options.minDate || ""
            });

            //handle disposal (if KO removes by the template binding)
            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                $(element).datepicker("destroy");
            });

        },
        update: function (element, valueAccessor) {
            var value = ko.utils.unwrapObservable(valueAccessor());
            $(element).val(value);
        }
    };

    ko.bindingHandlers.validationDependsOn = {
        update: function (element, valueAccessor) {
            var observable = valueAccessor();

            if (observable.isValid) {
                if (!observable.isValid() && observable.isModified()) {
                    $(element).addClass('errorFill');
                } else {
                    $(element).removeClass('errorFill');
                }
            }
        }
    };
    //Slider Binding Handler
    ko.bindingHandlers.slider = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            var sliderOptions = allBindingsAccessor().sliderOptions || {};
            $(element).slider(sliderOptions);
            ko.utils.registerEventHandler(element, "slidechange", function (event, ui) {
                var observable = valueAccessor();
                observable(ui.value);
            });
            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                $(element).slider("destroy");
            });
            ko.utils.registerEventHandler(element, "slide", function (event, ui) {
                var observable = valueAccessor();
                observable(ui.value);
            });
        },
        update: function (element, valueAccessor) {
            var value = ko.utils.unwrapObservable(valueAccessor());
            if (isNaN(value)) value = 0;
            $(element).slider("value", value);

        }
    };

    // date formatting. Example <div class="date" data-bind="dateString: today, datePattern: 'dddd, MMMM dd, yyyy'">Thursday, April 05, 2012</div>
    ko.bindingHandlers.dateString = {
        update: function (element, valueAccessor, allBindingsAccessor) {
            var value = valueAccessor(),
                allBindings = allBindingsAccessor();
            var valueUnwrapped = ko.utils.unwrapObservable(value);
            var pattern = allBindings.datePattern || ist.datePattern;
            if (valueUnwrapped !== undefined && valueUnwrapped !== null) {
                $(element).text(moment(valueUnwrapped).format(pattern));
            }
            else {
                $(element).text("");
            }

        }
    };


    //Custom binding for handling validation messages in tooltip
    ko.bindingHandlers.validationTooltip = {
        update: function (element, valueAccessor) {
            var observable = valueAccessor(), $element = $(element);
            if (observable.isValid) {
                if (!observable.isValid() && observable.isModified()) {
                    $element.tooltip({ title: observable.error }); //, delay: { show: 10000, hide: 10000 }
                } else {
                    $element.tooltip('destroy');
                }
            }
        }
    };

    //Custom binding for handling validation messages in tooltip
    ko.bindingHandlers.tooltip = {
        update: function (element, valueAccessor) {
            var $element = $(element);
            var value = ko.utils.unwrapObservable(valueAccessor());
            value = value.replace(/:0/g, ':00');
            $element.tooltip({ title: value, html: true }); //, delay: { show: 10000, hide: 10000 }
        }
    };

    // Forcing Input to be Numeric
    ko.extenders.numeric = function (target, precision) {
        //create a writeable computed observable to intercept writes to our observable
        var result = ko.computed({
            read: target,  //always return the original observables value
            write: function (newValue) {
                var current = target(),
                    roundingMultiplier = Math.pow(10, precision),
                    newValueAsNum = isNaN(newValue) ? 0 : parseFloat(+newValue),
                    valueToWrite = Math.round(newValueAsNum * roundingMultiplier) / roundingMultiplier;

                //only write if it changed
                if (valueToWrite !== current) {
                    target(valueToWrite);
                } else {
                    //if the rounded value is the same, but a different value was written, force a notification for the current field
                    if (newValue !== current) {
                        target.notifySubscribers(valueToWrite);
                    }
                }
            }
        });

        //initialize with current value to make sure it is rounded appropriately
        result(target());

        //return the new computed observable
        return result;
    };

    //allow input to be only numeric and nullable
    ko.extenders.numericNullable = function (target, precision) {
        //create a writeable computed observable to intercept writes to our observable
        var result = ko.computed({
            read: target,  //always return the original observables value
            write: function (newValue) {
                var current = target(),
                    roundingMultiplier = Math.pow(10, precision),
                    newValueAsNum = isNaN(newValue) ? null : parseFloat(+newValue),
                    valueToWrite = Math.round(newValueAsNum * roundingMultiplier) / roundingMultiplier;

                //only write if it changed
                if (valueToWrite !== current) {
                    target(valueToWrite);
                } else {
                    //if the rounded value is the same, but a different value was written, force a notification for the current field
                    if (newValue !== current) {
                        target.notifySubscribers(valueToWrite);
                    }
                }
            }
        });

        //initialize with current value to make sure it is rounded appropriately
        result(target());

        //return the new computed observable
        return result;
    };

    // Forcing Input to be Numeric
    ko.extenders.number = function (target) {
        //create a writeable computed observable to intercept writes to our observable
        var result = ko.computed({
            read: function () {
                return target();
            },
            write: function (newValue) {
                var current = target(),
                    valueToWrite = newValue === null || newValue === undefined ? null : numeral().unformat("" + newValue);

                //only write if it changed
                if (valueToWrite !== current) {
                    target(valueToWrite);
                } else {
                    //if the rounded value is the same, but a different value was written, force a notification for the current field
                    if (newValue !== current) {
                        target.notifySubscribers(valueToWrite);
                    }
                }
            }
        });

        //initialize with current value to make sure it is rounded appropriately
        result(target());

        //return the new computed observable
        return result;
    };

    // number formatting setting the text property of an element
    ko.bindingHandlers.number = {
        update: function (element, valueAccessor, allBindingsAccessor) {
            var value = valueAccessor(),
                allBindings = allBindingsAccessor();
            var valueUnwrapped = ko.utils.unwrapObservable(value);
            var pattern = allBindings.format || ist.numberFormat;
            if (valueUnwrapped !== undefined && valueUnwrapped !== null) {
                var formattedValue = numeral(valueUnwrapped).format(pattern);
                $(element).text(formattedValue);
            }
            else {
                $(element).text("");
            }

        }
    };
    // number formatting for input fields
    ko.bindingHandlers.numberValue = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            var underlyingObservable = valueAccessor(),
                allBindings = allBindingsAccessor(),
                pattern = allBindings.numberFormat || ist.numberFormat;

            var interceptor = ko.computed({
                read: function () {
                    if (underlyingObservable() === null || underlyingObservable() === undefined || underlyingObservable() === "") {
                        return "";
                    }
                    // ReSharper disable InconsistentNaming
                    return new numeral(underlyingObservable()).format(pattern);
                    // ReSharper restore InconsistentNaming
                },

                write: function (newValue) {
                    var current = underlyingObservable(),
                        valueToWrite = newValue === null || newValue === undefined || newValue === "" ? null : numeral().unformat("" + newValue);

                    if (valueToWrite !== current) {
                        underlyingObservable(valueToWrite);
                    } else {
                        if (newValue !== current.toString())
                            underlyingObservable.valueHasMutated();
                    }
                }
            });

            ko.applyBindingsToNode(element, { value: interceptor });
        }
    };

    // Forcing Input to be Numeric
    ko.extenders.numberInput = function (target) {
        //create a writeable computed observable to intercept writes to our observable
        var result = ko.computed({
            read: function () {
                return target();
            },
            write: function (newValue) {
                var current = target(),
                    valueToWrite = newValue === null || newValue === undefined ? null : numeral().unformat("" + newValue);

                //only write if it changed
                if (valueToWrite !== current) {
                    target(valueToWrite);
                } else {
                    //if the rounded value is the same, but a different value was written, force a notification for the current field
                    if (newValue !== current) {
                        target.notifySubscribers(valueToWrite);
                    }
                }
            }
        });

        //initialize with current value to make sure it is rounded appropriately
        result(target());

        //return the new computed observable
        return result;
    };

    // number formatting setting the text property of an element
    ko.bindingHandlers.numberInput = {
        update: function (element, valueAccessor, allBindingsAccessor) {
            var value = valueAccessor(),
                allBindings = allBindingsAccessor();
            var valueUnwrapped = ko.utils.unwrapObservable(value);
            var pattern = allBindings.format || ist.numberFormat;
            if (valueUnwrapped !== undefined && valueUnwrapped !== null) {
                var formattedValue = numeral(valueUnwrapped).format(pattern);
                $(element).text(formattedValue);
            }
            else {
                $(element).text("");
            }

        }
    };

    // Jquery Knob Binding Handler
    ko.bindingHandlers.knob =
    {
        init: function (element, valueAccessor, allBindings) {
            var knobOptions = {};

            ko.utils.extend(knobOptions, allBindings.get('knobOptions'));
            ko.utils.extend(knobOptions, {
                change: function (v) {
                    var value = valueAccessor();
                    if (value() != v) {
                        value(v);
                    }
                },
                release: function (v) {
                    var value = valueAccessor();
                    if (value() != v) {
                        value(v);
                    }
                }
            });

            $(element)
                .knob(knobOptions)
                .trigger('configure', knobOptions);
            
        },
        update: function (element, valueAccessor, allBindings) {
            var local = ko.utils.unwrapObservable(valueAccessor()),
                knobOptions = {};

            ko.utils.extend(knobOptions, allBindings.get('knobOptions'));

            $(element)
                .trigger('configure', knobOptions)
                .val(local)
                .trigger('change');
        }
    };


    // KO Dirty Flag - Change Tracking
    ko.dirtyFlag = function (root, isInitiallyDirty) {
        var result = function () { },
    // ReSharper disable InconsistentNaming
            _initialState = ko.observable(ko.toJSON(root)),
            _isInitiallyDirty = ko.observable(isInitiallyDirty);
        // ReSharper restore InconsistentNaming

        result.isDirty = ko.dependentObservable(function () {
            return _isInitiallyDirty() || _initialState() !== ko.toJSON(root);
        });

        result.reset = function () {
            _initialState(ko.toJSON(root));
            _isInitiallyDirty(false);
        };

        return result;
    };
    // KO Dirty Flag - Change Tracking

    // Common View Model - Editor (Save, Cancel - Reverts changes, Select Item)
    ist.ViewModel = function (model) {

        //hold the currently selected item
        this.selectedItem = ko.observable();

        // hold the model
        this.model = model;

        //make edits to a copy
        this.itemForEditing = ko.observable();

    };

    ko.utils.extend(ist.ViewModel.prototype, {
        //select an item and make a copy of it for editing
        selectItem: function (item) {
            this.selectedItem(item);
            this.itemForEditing(this.model.CreateFromClientModel(ko.toJS(item)));
        },

        acceptItem: function (data) {

            //apply updates from the edited item to the selected item
            this.selectedItem().update(data);

            //clear selected item
            this.selectedItem(null);
            this.itemForEditing(null);
        },

        //just throw away the edited item and clear the selected observables
        revertItem: function () {
            this.itemForEditing().reset(); // Resets Changed State
            this.selectedItem(null);
            this.itemForEditing(null);
        }
    });

    // Common View Model

    // Used to show popover
    ko.bindingHandlers.bootstrapPopover = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {

            // ReSharper disable DuplicatingLocalDeclaration
            var options = valueAccessor();
            // ReSharper restore DuplicatingLocalDeclaration
            var node = $("#" + options.elementNode);
            var defaultOptions = { trigger: 'manual', content: node.html(), placement: "bottom" }; // Trigger Manual to do some checks before opening popover
            options = $.extend(true, {}, defaultOptions, options);
            $(element).popover(options);

            // Show Popover
            function showBootstrapPopover(self, elem, currentlyOpenedPopoverId) {
                var buttons = $("[id*='PopoverButton']");
                buttons.not(self).popover('hide');
                // If Current Popover needs to be closed
                if (currentlyOpenedPopoverId && currentlyOpenedPopoverId === options.popoverId) {
                    $(elem).popover('hide');
                }
                else {
                    $(elem).popover('show');
                }
            }

            $(element).click(function () {
                var self = this;
                //If it is to validate before opening another popover like being done in RA Screen
                if (options.doBeforeOpeningPopover && typeof options.doBeforeOpeningPopover === "function") {
                    options.doBeforeOpeningPopover(function (currentlyOpenedPopoverId) {
                        showBootstrapPopover(self, element, currentlyOpenedPopoverId);
                    });
                }
                else {
                    var popover = $('.popover');
                    var currentlyOpenedPopover = $('.popover .popoverContents');
                    var currentPopoverId = "";
                    if (popover.hasClass('in') && currentlyOpenedPopover && currentlyOpenedPopover[0]) {
                        currentPopoverId = currentlyOpenedPopover[0].id;
                    }
                    showBootstrapPopover(self, element, currentPopoverId);
                }
                var popOver = $("#" + options.popoverId);
                if (popOver) {
                    popOver = popOver[0];
                }
                var childBindingContext = bindingContext.createChildContext(viewModel);
                if (!popOver) {
                    return;
                }
                ko.cleanNode(popOver);
                ko.applyBindingsToDescendants(childBindingContext, popOver);
            });
        }
    };


    ko.bindingHandlers.bootstrapSwitchOn = {
        // ReSharper disable UnusedParameter
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            // ReSharper restore UnusedParameter
            var $elem = $(element);
            var switchOptions = allBindingsAccessor().switchOptions || {};
            // ReSharper disable UnusedLocals
            var defaultValue = ko.utils.unwrapObservable(valueAccessor());
            // ReSharper restore UnusedLocals
            $elem.bootstrapSwitch(switchOptions);
            $elem.on('switchChange.bootstrapSwitch', function (e, data) {
                valueAccessor()(data);
            }); // Update the model when changed.
        },
        // ReSharper disable UnusedParameter
        update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            // ReSharper restore UnusedParameter
            var vStatus = $(element).bootstrapSwitch('state');
            var vmStatus = ko.utils.unwrapObservable(valueAccessor());
            if (vStatus != vmStatus) {
                $(element).bootstrapSwitch('setState.bootstrapSwitch', vmStatus);
            }
        }
    };


    // Fix for bootstrap popovers, sometimes they are left in the DOM when they shouldn't be.
    $('body').on('hidden.bs.popover', function () {
        var popovers = $('.popover').not('.in');
        if (popovers) {
            popovers.remove();
        }
    });

    // Fix for bootstrap tooltips, sometimes they are left in the DOM when they shouldn't be.
    $('body').on('hidden.bs.tooltip', function () {
        var tooltips = $('.tooltip').not('.in');
        if (tooltips) {
            tooltips.remove();
        }
    });

    // Can be used to have a parent with one binding and children with another. Child areas should be surrounded with <!-- ko stopBinding: true --> <!-- /ko -->
    ko.bindingHandlers.stopBinding = {
        init: function () {
            return { controlsDescendantBindings: true };
        }
    };

    ko.virtualElements.allowedBindings.stopBinding = true;

    var options = { insertMessages: false, decorateElement: true, errorElementClass: 'errorFill', messagesOnModified: true, registerExtenders: true };
    ko.validation.init(options);

});


// Sorting 
// <Param>tableId - Id of the table like "productTable" </Param>
// <Param>Sort On - Observable, to identify sort column</Param>
// <Param>Sort Asc - Observable, to identify sort Order i.e. Asc, or desc</Param>
// <Param>callback - function, to call for refreshing the list</Param>
function handleSorting(tableId, sortOn, sortAsc, callback) {
    // Make Table Sortable
    $('#' + tableId + ' thead tr th span').bind('click', function (e) {
        if (!e.target || !e.target.id) {
            return;
        }
        var sortBy = e.target.id;
        var targetEl = $(e.target).children("span")[0];
        // Remove other header sorting
        _.each($('#' + tableId + ' thead tr th span'), function (item) {
            if (item.parentElement !== e.target) {
                item.className = '';
            }
        });
        // Sort Column
        if (targetEl) {
            var direction = (targetEl.className === 'icon-up') ? 'icon-up' : (targetEl.className === 'icon-down') ? 'icon-down' : 'icon-down';
            if (direction === 'icon-up') {
                targetEl.className = 'icon-down';
                sortAsc(false);
            } else {
                targetEl.className = 'icon-up';
                sortAsc(true);
            }
            sortOn(sortBy);

            // Refresh Grid
            if (callback && typeof callback === "function") {
                callback();
            }
        }
    });
}
//Model Year
modelYearsGlobal = [
    { Id: 1980, Text: '1980' },
    { Id: 1981, Text: '1981' },
    { Id: 1982, Text: '1982' },
    { Id: 1983, Text: '1983' },
    { Id: 1984, Text: '1984' },
    { Id: 1985, Text: '1985' },
    { Id: 1986, Text: '1986' },
    { Id: 1987, Text: '1987' },
    { Id: 1988, Text: '1988' },
    { Id: 1989, Text: '1989' },
    { Id: 1990, Text: '1990' },

    { Id: 1991, Text: '1991' },
    { Id: 1992, Text: '1992' },
    { Id: 1993, Text: '1993' },
    { Id: 1994, Text: '1994' },
    { Id: 1995, Text: '1995' },
    { Id: 1996, Text: '1996' },
    { Id: 1997, Text: '1997' },
    { Id: 1998, Text: '1998' },
    { Id: 1999, Text: '1999' },
    { Id: 2000, Text: '2000' },

    { Id: 2001, Text: '2001' },
    { Id: 2002, Text: '2002' },
    { Id: 2003, Text: '2003' },
    { Id: 2004, Text: '2004' },
    { Id: 2005, Text: '2005' },
    { Id: 2006, Text: '2006' },
    { Id: 2007, Text: '2007' },
    { Id: 2008, Text: '2008' },
    { Id: 2009, Text: '2009' },
    { Id: 2010, Text: '2010' },

    { Id: 2011, Text: '2011' },
    { Id: 2012, Text: '2012' },
    { Id: 2013, Text: '2013' },
    { Id: 2014, Text: '2014' },
    { Id: 2015, Text: '2015' },
    { Id: 2016, Text: '2016' },
    { Id: 2017, Text: '2017' },
    { Id: 2018, Text: '2018' },
    { Id: 2019, Text: '2019' },
    { Id: 2020, Text: '2020' }
];

// Date Conversion - e.g. Hijri to Gregorain
// Used IN Rental Agreement
function ConvertDates(dateTobeChanged, fromCalender, toCalender) {
    var calender = $.calendars.instance(fromCalender);
    var dateToBeChanged = calender.parseDate(ist.customShortDateWithFullYearPattern, dateTobeChanged);
    calender = $.calendars.instance(toCalender);
    var changedDate = calender.fromJD(dateToBeChanged.toJD());
    return calender.formatDate(ist.customShortDateWithFullYearPattern, changedDate);
}

$(function () {
    // Fix for bootstrap popovers, sometimes they are left in the DOM when they shouldn't be.
    $('body').on('hidden.bs.popover', function () {
        var popovers = $('.popover').not('.in');
        if (popovers) {
            popovers.remove();
        }
    });

    // Fix for bootstrap tooltips, sometimes they are left in the DOM when they shouldn't be.
    $('body').on('hidden.bs.tooltip', function () {
        var tooltips = $('.tooltip').not('.in');
        if (tooltips) {
            tooltips.remove();
        }
    });
});

