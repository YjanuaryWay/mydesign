window.Ajax = {
    get: function (url, Success) {
        return httpRequest("GET", url, null, Success).catch(e => console.error(e));
    },
    /**
     * 
     * @param {string} url
     * @param {string} data
     * @param {Function} Success
     */
    post: function (url, data, Success) {
        return httpRequest("POST", url, `form=${data}`, Success).catch(e => console.error(e));
    },
    delete: function (url, data) {
        return httpRequest("POST", url, `form=${data}`).catch(e => console.error(e));
    },
    setXHR: function (xhr) {
        xhr.timeout = 3000;
    }
};
window.httpRequest = function (method, url, data, Success) {
    return new Promise((yes, no) => {
        const http = new XMLHttpRequest();
        http.open(method, url, true);
        Ajax.setXHR(http);
        http.ontimeout = e =>
            no(`连接超时: ${method}: ${url}`);
        http.onerror = e =>
            no("错误：" + http.status);
        http.onload = (e) => {
            var type = http.getResponseHeader("content-type");
            var data = http.responseText;
            switch (type) {
                case "charset=UTF-8;application/json;":
                    data = JSON.parse(http.responseText);
            }
            Success instanceof Function ?
                yes(Success(data)) :
                yes(data)
        }
        http.setRequestHeader("content-type", "application/x-www-form-urlencoded; charset=UTF-8");
        http.send(data);
    })
};
