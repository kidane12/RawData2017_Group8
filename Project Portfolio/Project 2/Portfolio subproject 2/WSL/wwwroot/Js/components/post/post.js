﻿define(['jquery','knockout'], function ($,ko) {
    return function (params) {
        var t = ko.observable("sdfghj");
        var posts = ko.observableArray([]);

        $.getJSON("api/posts", data => {
            posts(data.items);
            //nextLink(data.next);
            //prevLink(data.prev);
        });

        return {
            t,
            posts
        };

    }
});