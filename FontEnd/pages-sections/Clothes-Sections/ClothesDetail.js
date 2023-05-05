import React, { useEffect, useState } from "react";
import styles from "/styles/jss/nextjs-material-kit/pages/landingPageSections/teamStyle.js";
import { makeStyles } from "@material-ui/core/styles";
import GridContainer from "/components/Grid/GridContainer.js";
import GridItem from "/components/Grid/GridItem.js";
import Card from "/components/Card/Card.js";
import CustomInput from "/components/CustomInput/CustomInput.js";
// nodejs library that concatenates classes
import classNames from "classnames";
import { Button, ButtonGroup, Input, Radio } from "@material-ui/core";
import { AddShoppingCartSharp, Label } from "@material-ui/icons";
const useStyles = makeStyles(styles);
const BASE_URL = "/img/clothes/"
export default function ClothesDetail(items) {
    const classes = useStyles();
    const [quantity,setQuantity] = useState(0);
    const size = items.items.size.split(",");

    const imageClasses = classNames(
        classes.imgRaised,
        classes.imgFluid
      );
    const hadleQuantity = e =>{
        const {value} = e.target
        setQuantity(value)
    }
    const addToCart = () => {
        var size = document.querySelector('input[name="size"]:checked').value;
    }
    return (
        <GridContainer>
            <GridItem xs={12} sm={12} md={3}>
                <Card plain>
                    <img
                    src= {BASE_URL + items.items.image}
                    alt="..."
                    className={imageClasses}
                    />
                </Card>
            </GridItem>
            <GridItem xs={12} sm={12} md={6}>
                <Card plain>
                   <h1>{items.items.clothesName}</h1>
                   <h2>${items.items.prices}.000 VNĐ</h2>
                </Card>
                <input type="number" id="quantity" min="0" max={items.items.quantity} style={{width: 120, height: 30}} onClick={hadleQuantity}/>
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