async function api(url, method = 'GET', body = null) {
    const options = {
        method: method,
        headers: {
            'Content-Type': 'application/json'
        }
    };

    if (body) {
        options.body = JSON.stringify(body);
    }

    const response = await fetch(url, options);

    console.log(response);

    let data = null;

    const contentType = response.headers.get("content-type");

    if (contentType && contentType.includes("application/json")) {
        data = await response.json();
    } else if (contentType && contentType.includes("text/plain")) {
        data = await response.text();
    }

    if (!response.ok) {
        AppToast.error(JSON.stringify(data) || "Something went wrong");
        return null;
    }

    return data;
}