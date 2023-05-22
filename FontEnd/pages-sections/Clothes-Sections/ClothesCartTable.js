import { useEffect } from "react";
import { useState } from "react";
import { ENDPOINTS, createAPIEndpoint } from "../../api";
import { Card, ListItem } from "@material-ui/core";
import classNames from "classnames";
import styles from "/styles/jss/nextjs-material-kit/pages/landingPageSections/teamStyle.js";
import { makeStyles } from "@material-ui/core/styles";
import numeral from "numeral";

const BASE_URL = "/img/clothes/"
const useStyles = makeStyles(styles);
export default function ClothesDetail(props) {
    const {clothes, UpdateSumMoney} = props;
    const [data, setData] = useState({image:"",name:"",prices:"", type:""});
    const classes = useStyles();
    const imageClasses = classNames(
        classes.imgRaised,
        classes.imgFluid
    );
    useEffect(()=> {
        createAPIEndpoint(ENDPOINTS.getCart)
    .fetchWithName(clothes.clothesId)
    .then(res => {
        setData(res.data)
    })
    },[])
    const UpdateSumMoneyfunc = (id) => {
        var checkedBoxes = document.querySelector('input[id=CB'+id+']');
        var money = parseInt(document.querySelector('td[id=money'+id+']').textContent.replaceAll(',',''));
        if(checkedBoxes.checked)
        {    
            UpdateSumMoney(money)
        }
        else
        {
            UpdateSumMoney(-money)
        }
    }
    return (
        <>
            <td>
                <input type="checkbox" value={clothes.id} id={"CB"+clothes.id} name="pick" onChange={()=>UpdateSumMoneyfunc(clothes.id)}></input>
            </td>
            <td style={{ width: "10%" }}>
                <Card>
                    <img
                        src={BASE_URL + data.image}
                        alt="..."
                        className={imageClasses} />
                </Card>
            </td>
            <td id={"name"+clothes.id}>
                {data.clothesName}
            </td>
            <td>
                {numeral(data.prices).format('0,0')} VNĐ
            </td>
            <td>
                {clothes.quantity}
            </td>
            <td className={data.type} id={"money"+clothes.id}>
                {numeral(clothes.quantity * data.prices).format('0,0')} VNĐ
            </td>
            <td>
                {clothes.status}
            </td>
        </>
    )
}