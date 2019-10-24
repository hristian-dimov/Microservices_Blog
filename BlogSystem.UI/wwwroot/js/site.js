(function($) {
    "use strict";

    $(document).ready(function() {

        getBlogPosts();
        getCategories();
    });

    $(document).on('click', '#create-blog-post #submit', function() {

        var name = $('#blog-post-name').val();

        var categories = $('input[type=checkbox][name=category]:checked').map(function (_, element) {

            return $(element).val();
        }).get();

        if (name.length === 0 || categories.length === 0) {

            alert('Please enter blog post name and at least one category!');

            return;
        }

        createBlogPost(name, categories);
    });

    function getBlogPosts() {

        $.ajax({
            url: "/blog/posts",
            type: "GET"
        }).done(function (data) {

            $('#blog-posts').html(data);
        });
    }

    function getCategories() {

        $.ajax({
            url: "/blog/categories",
            type: "GET"
        }).done(function (data) {

            $('#blog-post-categories').html(data);
        });
    }

    function createBlogPost(name, categories) {

        var data = {
            name: name,
            postCategories: categories.map(function(id) {

                return { id: id };
            })
        };

        $.ajax({
            url: "/blog/posts",
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data)
        }).done(function (data) {

            $('#blog-posts').html(data);

            // clear fields
            $('#blog-post-name').val('');
            $("input[type=checkbox][name=category]:checked").prop("checked", false);
        });
    }
})(jQuery);