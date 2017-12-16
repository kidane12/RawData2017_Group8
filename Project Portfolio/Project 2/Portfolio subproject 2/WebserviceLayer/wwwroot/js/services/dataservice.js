﻿define(['jquery', 'knockout'], function ($, ko) {

    var getPosts = (url, callback) => {
        if (url === undefined) {
            url = "api/posts/q";
        }
        $.getJSON(url, callback);
    };

    var getTfWords = (url, callback) => {
        if (url === undefined) {
            url = "api/wordtf/19";
        }
        $.getJSON(url, callback);
    };

    //getting the users
    var getUsers = (url, callback) => {
        if (url === undefined) {
            url = "api/user";
        }
        $.getJSON(url, callback);
    };

    var addPost = function (data) {
        $.ajax({
            url: '/api/posts/add',
            data: data,
            processData: false,
            contentType: false,
            type: 'POST'
        }).done(function (retdata) {
            alert(retdata);
            //todo: select and show created post
        });
        /*if (url === undefined) {
            url = "api/posts/add";
        }
        $.getJSON(url, callback);*/
    }
    var getPost = function (url, callback) {
        $.getJSON(url, data => {
            var post = {
                title: data.title,
                score: data.score,
                creationDate: data.creationDate,
                body: data.body
            }

            $.getJSON(data.answers, ans => {
                post.answers = ko.observableArray(ans);
                callback(post);
            });
        });
    };

    return {
        getUsers,
        getPosts,
        getPost,
        getTfWords
    };
});