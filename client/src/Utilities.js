import React from 'react';

import { Modal } from 'antd';

export async function getResponseMessage(response) {

    var message = response.statusText;

    const contentType = response.headers.get("content-type");

    if (contentType && contentType.indexOf("application/json") !== -1) {

        var data = await response.json();
        if (data) {
            if (data.message)
            {
                return message + ': ' + data.message;
            }
            else {
                 return message + ': ' + data;
            }
        }
    }
    else {

        var text = await response.text();
        if (text) {
            return message + ': ' + text;
        }
    }

    return message;
}

export function showConfirmDelete(onDelete, id) {

    showConfirm(onDelete, id, 'Confirm Delete', 'Are you sure your want to delete this item? This cannot be undone.');
}

export function showConfirm(onOk, id, title, content) {

    Modal.confirm({
        title: title,
        content: (
            <>
                {content}
            </>
        ),
        onOk() {

            return new Promise((resolve, reject) => {

                onOk(id);
                resolve();
            })
        },
        onCancel() { },
    });
}