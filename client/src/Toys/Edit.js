import React, { useState, useEffect, useRef } from 'react';
import { Redirect } from "react-router-dom";

import { Form, Button, Input, InputNumber, message } from 'antd';

import * as Utilities from '../Utilities';
import { API_ENDPOINT } from '../Environment';

const Edit = (props) => {

    let id = parseInt(props.match.params.id, 10);
    if (isNaN(id)) {
        id = -1
    }
    let toyId = parseInt(id, 10);

    const formRef = useRef(null);

    const [form] = Form.useForm();
    const [toList, setToList] = useState(false);
    const [toy, setToy] = useState({});

    useEffect(() => {
        
        if (toyId > -1) {

            var url = `${API_ENDPOINT}/${toyId}`;
        
                fetch(url,
                {
                    method: 'GET'
                })
                .then(res => res.json())
                .then(toy => {

                    setToy(toy);

                    if (formRef.current) {
                        form.resetFields(); // important, otherise initialValues won't work
                    }
                });
        }
    }, [toyId, form, formRef]);

    const handleCancel = () => {

        setToList(true);
    }

    const handleFinish = values => {
        
        console.log('values:', values);

        const key = 'saveToy';
        message.loading({ content: toyId ? 'Saving toy...' : 'Adding toy...', key, duration: 0 });

        var data = {
            id: toyId,
            name: values.name,
            price: `$${values.price}`
        }

        console.log('data:', data);

        var url = (toyId > -1) ? `${API_ENDPOINT}/${toyId}` : API_ENDPOINT;
        var method = (toyId > -1) ? 'PUT' : 'POST';

        fetch(url, {
            method: method,
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            body: JSON.stringify(data)
        }).then((response) => {
            if (response.status === 200) {
                
                setTimeout(() => {
                    message.success({ content: 'Toy updated.', key, duration: 2 });
                    setToList(true);
                }, 1500);
            }
            else {

                Utilities.getResponseMessage(response)
                .then(msg => {

                    setTimeout(() => {
                        message.error({ content: `Unable to update toy: ${msg}`, key, duration: 4 });
                    }, 1500);
                });
            }
        });
    };

    const handleFinishFailed = errorInfo => {
        console.log('Failed:', errorInfo);
    };

    const handleDeleteClick = () => {

        Utilities.showConfirmDelete(handleDelete);
    }

    const handleDelete = () => {

        const key = 'deleteMemberSession';
        message.loading({ content: 'Deleting toy...', key, duration: 0 });

        var url = `${API_ENDPOINT}/${toyId}`;

        fetch(url, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            }
        })
        .then((response) => {
            if (response.status === 200) {
                
                setTimeout(() => {
                    message.success({ content: 'Toy deleted.', key, duration: 2 });
                    setToList(true);
                }, 1500);
            }
            else {

                Utilities.getResponseMessage(response)
                .then(msg => {

                    setTimeout(() => {
                        message.error({ content: `Unable to delete toy: ${msg}`, key, duration: 4 });
                    }, 1500);
                });
            }
        });
    }

    const layout = {
        labelCol: { span: 8 },
        wrapperCol: { span: 16 },
      };

      const tailLayout = {
        wrapperCol: { offset: 8, span: 16 },
      };

    return (

        <>
            {toList ? <Redirect to={'/'} /> : null}

            <div>
                <Form
                    form={form}
                    ref={formRef}
                    {...layout}
                    name="edit_toy_form"
                    initialValues={{
                        name: (toy && toy.name) ? toy.name : '',
                        price: (toy && toy.price) ? toy.price.substring(1) : '',
                    }}
                    onFinish={handleFinish}
                    onFinishFailed={handleFinishFailed}
                >
                    <Form.Item {...tailLayout}>

                        <h1>{toyId > -1 ? 'Edit' : 'Add'} Toy</h1>

                    </Form.Item>
                    
                    <Form.Item name="name" label="Toy Name" rules={[ { required: true, }, ]}>

                        <Input />

                    </Form.Item>

                    <Form.Item name="price" label="Price ($)" rules={[ { required: true, }, ]}>

                        <InputNumber
                            precision={2}
                        />

                    </Form.Item>

                    <Form.Item {...tailLayout}>
                        
                        { toyId > -1 ? <Button htmlType="button" onClick={e => handleDeleteClick(e)} style={{ 'marginRight': '8px' }}>Delete Toy</Button> : null }

                        <Button htmlType="button" onClick={handleCancel} style={{ 'marginRight': '8px' }}>Cancel</Button>
                        <Button type="primary" htmlType="submit" style={{ 'marginRight': '8px' }}>Save</Button>

                    </Form.Item>

                </Form>
            </div>

        </>
    );
}

export default (Edit);