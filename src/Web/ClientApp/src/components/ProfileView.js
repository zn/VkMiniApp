import React, {Component} from 'react'
import {View, Panel, PanelHeader, Cell, Avatar, Group, Header, List} from '@vkontakte/vkui'
import Icon24Article from '@vkontakte/icons/dist/24/article';
import Icon24Settings from '@vkontakte/icons/dist/24/settings';
import Icon28Notifications from '@vkontakte/icons/dist/28/notifications';

class ProfileView extends Component{
    
    render(){
        const {id, fetchedUser} = this.props
        return (
            <View id={id} activePanel="profile">
                <Panel id="profile">
                    <PanelHeader>Профиль</PanelHeader>
                    {fetchedUser && <Group>
                        <Cell
                            description="Рейтинг: 424"
                            bottomContent="Объявлений: 12"
                            before={<Avatar src={fetchedUser.photo_200} size={80}/>}
                            size="l"
                            >
                            {fetchedUser.first_name} {fetchedUser.last_name}
                        </Cell>
                    </Group>}   
                    <Group header={<Header mode="secondary">Действия</Header>}>
                        <List>
                            <Cell before={<Icon24Article />}>Мои объявления</Cell>
                            <Cell before={<Icon28Notifications />}>Уведомления</Cell>
                            <Cell before={<Icon24Settings />}>Настройки</Cell>
                        </List>
                    </Group> 
                </Panel>
            </View>
        )
    }
}

export default ProfileView