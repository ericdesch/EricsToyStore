import React, { useState, useEffect } from 'react';
import { Redirect } from "react-router-dom";

import { Table, Button } from 'antd';

import { API_ENDPOINT } from '../Environment';

const List = (props) => {

    const [loading, setLoading] = useState(false);
    const [toys, setToys] = useState([]);
    const [toToy, setToToy] = useState(false);
    const [toyUrl, setToyUrl] = useState();

    useEffect(() => {

        const loadToys = async () => {

            setLoading(true);

            var toysResult = await fetch(API_ENDPOINT,
            {
                method: 'GET'
            });

            const toys = await toysResult.json();

            setLoading(false);
            setToys(toys);
        }

        loadToys();
 
    }, []);

    const handleAdd = (record) => {

        setToToy(true);
        setToyUrl('/add');
    }
    
    const rowClicked = (record) => {

        setToToy(true);
        setToyUrl(`/edit/${record.id}`);
    }

    const columns = [
        {
            title: 'Toy Name',
            dataIndex: 'name',
        },
        {
            title: 'Price',
            dataIndex: 'price',
        },
    ];

    return (

        <>
            {toToy ? <Redirect to={toyUrl} /> : null}

            <div>

                <Table
                    dataSource={toys}
                    columns={columns}
                    showSorterTooltip={false}
                    rowKey={record => record.id}
                    loading={loading}
                    onRow={(record) => ({
                        onClick: () => {
                            rowClicked(record);
                        },
                    })}
                    pagination={false}
                />

                <Button type="primary" htmlType="button" onClick={handleAdd} style={{ 'marginTop': '20px' }}>Add Toy</Button>

            </div>
        </>
    );
}

export default (List);