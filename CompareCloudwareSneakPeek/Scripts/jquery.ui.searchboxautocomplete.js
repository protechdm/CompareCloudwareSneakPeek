/*
* jQuery UI SearchBox AutoComplete version 1.0
* 
* Copyright 2011, AUTHOR slavko.parezanin@gmail.com
* jQuery widget for autocompletion of categorized data through the hierarchies
* 
* Depends:
*	jquery.ui.core.js
*	jquery.ui.widget.js
*	jquery.ui.position.js
*	jquery.ui.autocomplete.js
*/

(function ($, undefined) {

    $.widget("ui.searchboxautocomplete", $.ui.autocomplete, {
        _renderMenu: function (ul, items) {
            var self = this,
				currentCategory = "";
            //self.append("<div>");
            $.each(items, function (index, item) {
                if (index == 0) {
                    //item.option.selected = true;
                    //ul.append("<li class='ui-menu-item'" + " role='menuitem'" + ">" + "SEARCH RESULTS" + "</li>");
                    //ul.append("<li class='ui-menu-item'" + " role='menuitem'" + ">" + "SEARCH RESULTS" + "</li>");
                }
                else if (item.category != currentCategory) {
                    ul.append("<li class='ui-autocomplete-category font-bold-13px-black'><div><label>" + item.category + "</label></div></li>");
                    currentCategory = item.category;
                }
                self._renderItem(ul, item);
            });
            //selectFirst: true;
            //self.append("</div>");
        },
        _create: function () {
            var self = this;
            //$.widget.bridge stores the plugin instance, even before the _create function runs
            //Don't navigate away from the field on tab when selecting an item
            this.element
            //                                .bind("keydown", function (event) {
            //                                    if (event.keyCode === $.ui.keyCode.TAB && self.menu.active) {
            //                                        event.preventDefault();
            //                                    }
            //                                    else if (event.keyCode === $.ui.keyCode.BACKSPACE || event.keyCode === $.ui.keyCode.DELETE) {
            //                                        logIt("options.termBefore:" + $(this).val());
            //                                        self.options.termBefore = $(this).val();
            //                                    }
            //                                })
            //                                .bind("keyup", function (event) {
            //                                    if (event.keyCode === $.ui.keyCode.BACKSPACE || event.keyCode === $.ui.keyCode.DELETE) {
            //                                        if (self.options.termBefore != $(this).val())
            //                                            self.onDelete();
            //                                    }
            //                                })
                                .bind("focus", function () {
                                    if ($(this).val() == "Search")
                                        $(this).val("");
                                })
                                .bind("blur", function () {
                                    if ($(this).val() == "")
                                        $(this).val("Search");
                                })
            // call the base class
            $.ui.autocomplete.prototype._create.apply(this, arguments);
        },
        _init: function () {
            $.ui.autocomplete.prototype._init.apply(this, arguments);
            //this.options.separator = $.ui.searchboxautocomplete.separator;
            //this.options.sepChar = this.options.separator[0];
            var self = this;
            this.element.bind("searchboxautocompleteclose", function () {
                self.options.offeringSecondLevel = false;
            });
        },
        options: {
            select: function (event, ui) {
                var chosenID = ui.item.tagModel.CloudApplicationModel.CloudApplicationID;
                $('#chosenID').val(chosenID);
                $('#searchText').val(ui.item.value);
                $('#searchbutton').trigger('click');
                //var self = $(this).data("searchboxautocomplete");
                var self = $(this).data("ui-searchboxautocomplete");
                var terms = $.ui.searchboxautocomplete.split(this.value);
                // remove the current input
                var lastInput = self.options.offeringSecondLevel ? "" : terms.pop();
                // add the selected item
                terms.push(ui.item.labelShort); //.value
                // add placeholder to get the separator at the end
                terms.push("");
                var count = self.options.addRow(ui.item);
                this.value = terms.join(self.options.separator);
                //                if (count == 1 || count > 1 && $.trim(lastInput) == "")
                //                    setTimeout(function () {
                //                        self.options.offeringSecondLevel = true;
                //                        self._search("SecondLevel");
                //                    }, 10);
                //                return false;
            },
            //            cache: {},
            //            lastXhr: null,
            //            getCachedData: function (term) {
            //                if (term in this.cache) {
            //                    return this.removeSelected(this.cache[term]);
            //                }
            //                return null;
            //            },
            //            setCachedData: function (term, data) {
            //                if (term.length > 1) {
            //                    //debugger;
            //                    logIt("term:" + term + " is cached!")
            //                    this.cache[term] = data;
            //                }
            //            },

            // per element instance
            autocompleteRows: [],

            open: function () {
                //var autocomplete = $(this).data("searchboxautocomplete"),
                var autocomplete = $(this).data("ui-searchboxautocomplete"),
                menu = autocomplete.menu;
                if (!autocomplete.options.selectFirst) {
                    return;
                }
                var firstElement = menu.element.children().first();
                //menu.activate($.Event({ type: "mouseenter" }), menu.element.children().first());
            },

            addRow: function (item) {
                this.autocompleteRows.push(item)
                return this.autocompleteRows.length;
            },
            //            deleteRow: function (i) {
            //                this.autocompleteRows.splice(i, 1);
            //            },
            //            removeSelected: function (data) {
            //                var rows = this.autocompleteRows;
            //                var length = rows.length;
            //                return $.grep(data, function (d) {
            //                    for (var i = 0; i < length; i++)
            //                        if (d.category === rows[i].category && d.value === rows[i].value)
            //                            return false;
            //                    return true;
            //                });
            //            },

            termBefore: "",
            separator: ": ",
            sepChar: ":",
            offeringSecondLevel: false,
            //$.ui.autocomplete({ autoFocus: true });
            selectFirst: true

        }
        //        onDelete: function () {
        //            var termBefore = $.trim(this.options.termBefore);
        //            var termNow = $.trim(this.element.val());
        //            if (termBefore == termNow) // last blank deleted
        //                return;
        //            var arrBefore = $.ui.searchboxautocomplete.split(termBefore);
        //            var arrNow = $.ui.searchboxautocomplete.split(termNow);
        //            var sep = this.options.separator;
        //            var sepChar = this.options.sepChar;
        //            // verify if removed char if sepChar at the end of query
        //            if (termBefore.length - termNow.length == 1 &&
        //                    termBefore[termBefore.length - 1] == sepChar &&
        //                    termNow[termNow.length - 1] != sepChar) {
        //                arrNow[arrNow.length - 1] = "";
        //            }
        //            //
        //            var arr = $.grep(arrBefore, function (val) {
        //                return $.inArray(val, arrNow) >= 0;
        //            });
        //            this.options.autocompleteRows = $.grep(this.options.autocompleteRows, function (row) {
        //                return $.inArray(row.labelShort, arr) >= 0;
        //            });
        //            arr.push("");
        //            this.element.val(arr.join(this.options.separator));                
        //        }
    });

    // can be called without instance
    $.extend($.ui.searchboxautocomplete, {
        separator: ": ",  // separator
        re: null,
        split: function (val) {
            if (this.re == null)
                this.re = new RegExp(this.separator[0] + "\\s*");
            return val.split(this.re);   // 
        },
        extractLast: function (term) {
            return this.split(term).pop();
        }
    });

    $.fn.setSearchText = function (rows) {
        return this.each(function () {
            var sep = $.ui.searchboxautocomplete.separator;
            var freeTextSearch = "";
            if (rows.length > 0 && rows[rows.length - 1].category == "freeTextSearch")
                freeTextSearch = rows.pop().labelShort; // remove the last row
            var str = $.map(rows, function (row) { return row.labelShort; }).join(sep) +
                        (rows.length > 0 ? sep : "") +
                        freeTextSearch;
            if (str == "")
                $(this).val("Search");
            else
                $(this).val(str).focus().caret(str.length, str.length);
        });
    }

    /*
    * Copyright (c) 2007-2008 Josh Bush (digitalbush.com)
    * 
    * Version: 1.1.3
    * Release: 2008-04-16
    */
    //Helper Function for Caret positioning
    $.fn.caret = function (begin, end) {
        if (this.length == 0) return;
        if (typeof begin == 'number') {
            end = (typeof end == 'number') ? end : begin;
            return this.each(function () {
                if (this.setSelectionRange) {
                    this.focus();
                    this.setSelectionRange(begin, end);
                } else if (this.createTextRange) {
                    var range = this.createTextRange();
                    range.collapse(true);
                    range.moveEnd('character', end);
                    range.moveStart('character', begin);
                    range.select();
                }
            });
        } else {
            if (this[0].setSelectionRange) {
                begin = this[0].selectionStart;
                end = this[0].selectionEnd;
            } else if (document.selection && document.selection.createRange) {
                var range = document.selection.createRange();
                begin = 0 - range.duplicate().moveStart('character', -100000);
                end = begin + range.text.length;
            }
            return { begin: begin, end: end };
        }
    };


    //    $(".ui-autocomplete-input").live("autocompleteopen", function () {
    $(".ui-autocomplete-input").on("autocompleteopen", function () {
        //    $("body").on("autocompleteopen", ".ui-autocomplete-input", function () {
        //$(".ui-autocomplete-input").bind('autocompleteopen', function () {
        //var autocomplete = $(this).data("autocomplete"),
        var autocomplete = $(this).data("ui-autocomplete"),
        menu = autocomplete.menu;
        if (!autocomplete.options.selectFirst) {
            return;
        }
        //menu.activate($.Event({ type: "mouseenter" }), menu.element.children().first());
    });

} (jQuery));


(function ($) {


} (jQuery));