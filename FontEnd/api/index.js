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
    UpdateOrder:'Order/OrderId'
}
export const createAPIEndpoint = endpoint =>{
    let url = BASE_URL + 'api/' + endpoint + '/';
    return {
        fetch:()=> axios.get(url, {withCredentials: true }),
        fetchWithName: (id) => axios.get(url, {withCredentials: true,
            params:{
            ClothesId : id
        }}),
        post: data => axios.post(url, data, { withCredentials: true  }),
        put: (updatedRecord) => axios.put(url, updatedRecord, { withCredentials: true}),
        delete:()=>axios.delete(url, {withCredentials: true }),
        getLogin: (data) => axios.post(url,data,{withCredentials: true}),
        postCreateAccount: (data) => axios.post(url,data),
    }
}