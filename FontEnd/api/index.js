import axios from 'axios'
export const BASE_URL = 'https://localhost:7091/';
export const ENDPOINTS = {
    createAccount: 'Account/CreateAccount',
    login: 'Account/Login',
    createPost:'CreatePost',
    getClothes:'Clothes'
}
export const createAPIEndpoint = endpoint =>{
    let url = BASE_URL + 'api/' + endpoint + '/';
    return {
        fetch:()=> axios.get(url, {withCredentials: true }),
        fetchwithName: (name) => axios.get(url+"ClothesId", {withCredentials: true,
            params:{
            ClothesId : name
        }}),
        post: newRecord => axios.post(url, newRecord, { withCredentials: true  }),
        put: (updatedRecord) => axios.put(url, updatedRecord, { withCredentials: true }),
        delete:()=>axios.delete(url, {withCredentials: true }),
        getLogin: (data) => axios.post(url,data,{withCredentials: true}),
        postCreateAccount: (data) => axios.post(url,data),
    }
}