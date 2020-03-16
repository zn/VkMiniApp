import React, {Component} from 'react'
import {View, Panel, PanelHeader, Div} from '@vkontakte/vkui'

class FeedView extends Component{
    
    state = {
        elements:[]
    }

    componentDidMount(){
        fetch('https://localhost:5001/api/Home')
        .then(result => {
            return result.json()
        })
        .then(data=>{
            this.setState({
                elements: data.slice()
            })
        })
    }

    render(){
        const {id} = this.props
        return (
            <View id={id} activePanel="feed">
                <Panel id="feed">
                    <PanelHeader>Лента</PanelHeader>
                    {this.state.elements.map(elem=><Div key={elem}>{elem}</Div>)}
                </Panel>
            </View>
        )
    }
}

export default FeedView