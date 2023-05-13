import React, { useState } from "react";
import styles from "/styles/jss/nextjs-material-kit/pages/landingPageSections/teamStyle.js";
import { makeStyles } from "@material-ui/core/styles";
import GridContainer from "/components/Grid/GridContainer.js";
import GridItem from "/components/Grid/GridItem.js";
import Card from "/components/Card/Card.js";
// nodejs library that concatenates classes
import classNames from "classnames";
import { Button } from "@material-ui/core";
import { AddShoppingCartSharp } from "@material-ui/icons";
import { ENDPOINTS, createAPIEndpoint } from "../../api";
const useStyles = makeStyles(styles);
const BASE_URL = "/img/clothes/"
export default function ClothesDetail(detail) {
    const classes = useStyles();
    const [quantity,setQuantity] = useState(0);
    const size = detail.clothes.size.split(",");

    const imageClasses = classNames(
        classes.imgRaised,
        classes.imgFluid
      );
    const handleQuantity = e =>{
        const {value} = e.target
        if (value > detail.clothes.quantity)
        {
            document.querySelector('input[name="quantity"]').value = detail.clothes.quantity
        }
        else if (value < 0)
        {
            document.querySelector('input[name="quantity"]').value = 0
        }
        setQuantity(value)
    }
    const addToCart = () => {
        var size = document.querySelector('input[name="size"]:checked').value;
        createAPIEndpoint(ENDPOINTS.Order)
        .post({
            AccountId: 1,
            ClothesId: detail.clothes.id,
            Quantity: quantity,
            Size: size,
            Status: "Trong giỏ hàng"
        })
        .then(res =>{
            console.log(res.data)
        })
    }
    return (
        <GridContainer>
            <GridItem xs={12} sm={12} md={3}>
                <Card plain>
                    <img
                    src= {BASE_URL + detail.clothes.image}
                    alt="..."
                    className={imageClasses}
                    />
                </Card>
            </GridItem>
            <GridItem xs={12} sm={12} md={6}>
                <Card plain>
                   <h1>{detail.clothes.clothesName}</h1>
                   <h2>${detail.clothes.prices}.000 VNĐ</h2>
                </Card>
                <input type="number" id="quantity" name="quantity" min="0" max={detail.clothes.quantity} style={{width: 120, height: 30}} onChange={handleQuantity}/>
                <br/>
                {size.map(item => {
                    return (
                        <div key={item}>
                            <input type="radio" id={item} name="size" value={item}/>
                            <label htmlFor={item}>{item}</label><br/>
                        </div>
                    )
                })}
                 <br/>
                <Button color="primary" style={{marginTop: "10px"}} onClick={addToCart}><AddShoppingCartSharp/> ADD TO CART</Button>
            </GridItem>
        </GridContainer>
    )
}