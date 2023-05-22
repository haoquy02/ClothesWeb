import axios from 'axios'
export const BASE_URL = 'https://localhost:7091/';
export const ENDPOINTS = {
    createAccount: 'Account/CreateAccount',
    login: 'Account/Login',
    createPost:'CreateClothes',
    getAllClothes:'Clothes',
    getClothes:'Clothes/ClothesId',
    getCart:'Clothes/Cart',
    Order:'Order',
    UpdateOrder:'Order/OrderId',
    Voucher:'Voucher'
}
export const createAPIEndpoint = endpoint =>{
    let url = BASE_URL + 'api/' + endpoint + '/';
    return {
        fetch:()=> axios.get(url, {withCredentials: true }),
        fetchWithName: (data) => axios.get(url, {withCredentials: true,
            params:{
            ClothesId : data
        }}),
        fetchWithVoucher: (data) => axios.get(url, {withCredentials: true,
            params:{
            code : data
        }}),
        postWithVoucher: (data) => axios.post(url, {}, {withCredentials: true,
            params:{
            voucherId : data
        }}),
        post: data => axios.post(url, data, { withCredentials: true  }),
        put: (updatedRecord) => axios.put(url, updatedRecord, { withCredentials: true}),
        delete:()=>axios.delete(url, {withCredentials: true }),
        getLogin: (data) => axios.post(url,data,{withCredentials: true}),
        postCreateAccount: (data) => axios.post(url,data),
    }
}