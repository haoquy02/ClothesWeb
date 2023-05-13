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
export default function ClothesDetail(clothes) {
    const [data, setData] = useState({image:"",name:"",prices:""});
    const classes = useStyles();
    const imageClasses = classNames(
        classes.imgRaised,
        classes.imgFluid
    ); 
    useEffect(()=> {
        createAPIEndpoint(ENDPOINTS.getCart)
    .fetchWithName(clothes.detail.clothesId)
    .then(res => {
        
        setData(res.data)
    })
    },[])
    return (
        <>
            <td>
                <input type="checkbox" value={clothes.detail.id} name="pick"></input>
            </td>
            <td style={{ width: "10%" }}>
                <Card>
                    <img
                        src={BASE_URL + data.image}
                        alt="..."
                        className={imageClasses} />
                </Card>
            </td>
            <td>
                {data.clothesName}
            </td>
            <td>
                {data.prices},000 VNĐ
            </td>
            <td>
                {clothes.detail.quantity}
            </td>
            <td>
                {numeral(clothes.detail.quantity * data.prices).format('0,0')},000 VNĐ
            </td>
            <td>
                {clothes.detail.status}
            </td>
        </>
    )
}